using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //[총알 발사 제작]
    // 목표 : 총알을 만들어서 발사하고 싶다.
    // 속성:
    // - 총알
    // - 총구 위치
    // 구현 순서
    //1. 발사 버튼을 누르면
    //2. 프리팹으로부터 총알을 동적으로 만들고,
    //3. 만든 총알의 위치를 총구의 위치로 바꾼다.

    [Header("총알 프리팹")]
    public GameObject BulletPrefab; //총알 프리팹
    public GameObject SubBulletPrefab; //보조총알 프리팹

    // 목표: 태어날 때 풀에다가 총알을 (풀 사이즈)개 생성한다. //디자인 패턴 - 오브젝트 풀 //풀:웅덩이
    // 속성:
    // - 풀 사이즈
    public int PoolSize = 20;
    // - 오브젝트(총알) 풀 
    private List<Bullet> _bulletPool = null;
    
    // 순서:
    // 1. 태어날 때: Awake
    private void Awake()
    {
        // 2. 오브젝트 풀 할당해주고..
        _bulletPool = new List<Bullet>();

        // 3. 총알 프리팹으로부터 총알을 풀 사이즈만큼 생성해준다.
        // 3-1. 메인 총알
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.SetActive(false); // 끈다.

            // 4. 생성한 총알을 풀에다가 넣는다. 
            _bulletPool.Add(bullet.GetComponent<Bullet>());
        }

        // 3-2. 서브 총알
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bullet = Instantiate(SubBulletPrefab);
            bullet.SetActive(false);

            // 4. 생성한 총알을 풀에다가 넣는다. 
            _bulletPool.Add(bullet.GetComponent<Bullet>());
            
        }
    }


    [Header("총구들")]
    public List<GameObject> Muzzles; //총구들
    public List<GameObject> SubMuzzles; // 보조 총구들

    [Header("타이머")]
    public float Timer = 10f;
    public const float COOL_TIME = 0.6f;

    public float BoomTimer = 0f;
    public const float BOOM_COOL_TIME = 5f;

    [Header("자동 모드")]
    public bool AutoMode = false;

    public AudioSource FireSource; // AudioSource: 오디오 클립을 재생시켜주는 컴포넌트

    public GameObject SpecialMoveBoomPrefab; //필살기붐
    private void CheckAutoMode()
    {
        // 1. 1/2 버튼 입력을 판단한다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 2. 1번 키→ 자동 공격 모드
            //Debug.Log("자동 공격 모드");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 3. 2번 키→ 수동 공격 모드
            //Debug.Log("수동 공격 모드");
            AutoMode = false;
        }
    }

    private void Start()
    {
        Timer = 0f;
        AutoMode = false;
    }

    void Update()
    {
        CheckAutoMode();
        

        // 타이머 계산
        Timer -= Time.deltaTime;
        BoomTimer -= Time.deltaTime;

        //1. 타이머가 0보다 작은 상태에서 발사 버튼을 누르면
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            //todo: 사운드 (기록해두어서 todo키워드로 금방 찾기)
            FireSource.Play();

            // 타이머 초기화
            Timer = COOL_TIME;

            //2. 프리팹으로부터 총알을 동적으로 만들고,
            //GameObject bullet = Instantiate(BulletPrefab); // Instantiate = 인스턴스화(객체화 비슷) <- 새로운 것 딱 하나.
            //GameObject bullet2 = Instantiate(BulletPrefab2);

            //3. 만든 총알의 위치를 총구의 위치로 바꾼다.
            //bullet.transform.position = Muzzle.transform.position;
            //bullet2.transform.position = Muzzle2.transform.position;

            // 목표: 총구 개수 만큼 총알을 풀에서 꺼내쓴다.
            // 순서:
            for (int i = 0; i < Muzzles.Count; i++)
            {
                // 1. 꺼져 있는 총알을 꺼낸다.
                Bullet bullet = null;

                foreach (Bullet b in _bulletPool)
                {
                    // 만약에 꺼져(비활성화되어) 있고 && 메인 총알이라면..
                    if (b.gameObject.activeInHierarchy == false && b.BType == BulletType.Main)
                    {
                        bullet = b;
                        break; // 찾았기 때문에 그 뒤까지 찾을 필요가 없다.
                    }
                }


                // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다.
                bullet.transform.position = Muzzles[i].transform.position;

                // 3. 총알을 킨다. (발사한다)
                bullet.gameObject.SetActive(true);
            }
                foreach (GameObject subMuzzle in SubMuzzles)
                    {
                    // 1. 꺼져 있는 총알을 찾아 꺼낸다.
                    Bullet bullet = null;
                    foreach (Bullet b in _bulletPool)
                    {
                        // 만약에 꺼져(비활성화되어) 있고 && 서브 총알이라면..
                        if (b.gameObject.activeInHierarchy == false && b.BType == BulletType.Sub)
                        {
                            bullet = b;
                            break; // 찾았기 때문에 그 뒤까지 찾을 필요가 없다.
                        }
                    }

                    // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다.
                    bullet.transform.position = subMuzzle.transform.position;

                    // 3. 총알을 킨다. (발사한다)
                    bullet.gameObject.SetActive(true);




                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && BoomTimer <= 0f)
            {
                // 타이머 초기화
                BoomTimer = BOOM_COOL_TIME;
                // 3번 키→ 필살기 공격 모드
                GameObject vfx = Instantiate(SpecialMoveBoomPrefab);
                vfx.transform.position = new Vector2(0, 0);
            }
            // if (BoomTimer <= 2f)
            {
                //Destroy(SpecialMoveBoomPrefab);
            }
        }
    }


