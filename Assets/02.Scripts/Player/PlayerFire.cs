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
    [Header("보조총알 프리팹")]
    public GameObject SubBulletPrefab; //총알 프리팹

    [Header("총구들")]
    public GameObject[] Muzzles; //총구들
    public GameObject[] SubMuzzles;

    [Header("타이머")]
    public float Timer = 10f;
    public const float COOL_TIME = 0.6f;

    public float BoomTimer = 5f;
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
            Debug.Log("자동 공격 모드");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 3. 2번 키→ 수동 공격 모드
            Debug.Log("수동 공격 모드");
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

            // 목표: 총구 개수 만큼 총알을 만들고, 만든 총알의 위치를 각 총구의 위치로 바꾼다.
            for (int i = 0; i < Muzzles.Length; i++)
            {
                // 1. 총알을 만들고
                GameObject bullet = Instantiate(BulletPrefab);
                GameObject bullet2 = Instantiate(SubBulletPrefab);

                // 2. 위치를 설정한다.
                bullet.transform.position = Muzzles[i].transform.position;
                bullet2.transform.position = SubMuzzles[i].transform.position;
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
