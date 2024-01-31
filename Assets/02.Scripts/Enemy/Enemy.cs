using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType // 적 타입 '열거형'
{
    Basic, // EnemyType.Basic 타입: 아래로 이동,
    Target,  // EnemyType.Target 타입: '처음' 태어났을 때 플레이어가 있는 방향으로 이동
    Follow
}
public class Enemy : MonoBehaviour
{
    public int Health = 2;

    public GameObject ItemPrefab_Health;
    public GameObject ItemPrefab_Speed;

    // 목표: 적을 아래로 이동시키고 싶다.
    // 속성:
    // - 속력

    public float Speed = 3f; // 이동 속도: 초당 3unit만큼 이동하겠다.

    public const float MinX = -3f;
    public const float MaxX = 3f;

    /***/
    public EnemyType EType; // Basic vs. Target 타입
    public Vector2 _dir; //방향 구하자
    private GameObject _target;


    public Animator MyAnimator;

    public GameObject ExplosionVFXPrefab;//참조


    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();

    }

    /*
    // 목표:
    // EnemyType.Basic 타입: 아래로 이동,
    // EnemyType.Target 타입: 처음 태어났을 때 플레이어가 있는 방향으로 이동
    // 속성
    // - EnemyType 타입
    // 구현 순서:
    // 1. 시작할 때 방향을 구한다. (플레이어가 있는 방향)
    // 2. 방향을 향해 이동한다.
    */


    // 시작할 때
    void Start()
    {
        // 캐싱: 자주 쓸법한 데이터를 더 가까운 장소에 저장해두고 필요할 때 가져다 쓰는 거 
        // 시작할 때 플레이어를 찾아서 기억해둔다. 
        _target = GameObject.Find("Player");

        ///MyAnimator 

        if (EType == EnemyType.Target)
        {
            // 1. 시작할 때 방향을 구한다. (플레이어가 있는 방향)
            // 1-1. 플레이어를 '찾는다'.
            //GameObject.FindGameObjectWithTag("Player");


            // 1-2. 방향을 '구한다'.
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize(); // 방향의 크기를 1로 만든다. 밑과 이름만 같지, 공유못한다. 


            // 1. 각도를 구한다. 
            //tanθ = y/x    ->  θ = y/x * atan(탄젠트의 역함수)
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            //Debug.Log(radian); // '호'도법 -> 라디안 값
            float degree = radian * Mathf.Rad2Deg;
            //Debug.Log(degree);

            // 2. 각도에 맞게 회전한다.
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree+90)); //이미지 리소스를 따라 90도 더해줌
                                                                                 // transform.LookAt(_target.transform); <-3d에서 씀. 선호x
            transform.eulerAngles = new Vector3(0, 0, degree + 90);
        }

        else // Basic 타입
        {
            _dir = Vector2.down;
        }

    }
    
    void Update()
    {
        //구현 순서
        // 2. 이동한다. 
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;

        //10 % 확률로 적이 날 따라오는 Follow형 적 생성하기
        if (EType == EnemyType.Follow)
        {
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();

            // 1. 각도를 구한다. 
            //tanθ = y/x    ->  θ = y/x * atan(탄젠트의 역함수)
            float radian = Mathf.Atan2(_dir.y, _dir.x); // '호'도법 -> 라디안 값
            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. 각도에 맞게 회전한다.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); //이미지 리소스를 따라 90도 더해줌
                                                                                  
        }


        // 애니메이터에게 파리미터(매개변수) 값을 넘겨준다. 
        // MyAnimator.SetInteger("h", (int)_dir); (x)

    }

    /*
    // 목표: 충돌하면 적과 플레이어를 삭제하고 싶다.
    // 구현 순서:
    // 1. 만약에 충돌이 일어나면
    // 2. 적과 플레이어를 삭제한다.
    */

    // 충돌이 일어나면 호출되는 이벤트 함수
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 1.충돌을 시작했을 때
        //Debug.Log("Enter");
        

        // 충돌한 게임 오브젝트의 태그를 확인
        //Debug.Log(collision.collider.tag); // <- Player or Bullet


        // 2.적과 플레이어를 삭제
        // 나죽자(나 자신)
        //Destroy(this.gameObject); //enemy 무조건죽음, 보조총알에는 2번만에 죽어야함
        
        // 플레이어는 적과 3번 충돌하면 죽게 만들기
        if (collision.collider.CompareTag("Player"))
        {
            Death();

            // 플레이어 스크립트를 가져온다.
            Player player = collision.collider.GetComponent<Player>();
            // 플레이어 체력을 -= 1
            //player.SetHealth(player.GetHealth() - 1);
            player.DecreaseHealth(1);

            // 플레이어 체력이 적다면..
            if (player.GetHealth() <= 0)
            {
                //Destroy(collision.collider.gameObject);
                collision.gameObject.SetActive(false);
            }

        }
        else if (collision.collider.CompareTag("Bullet")) //Tag //enemy와 총알이 부딪혔을 때
        {
            // 2. 충돌한 enemy를 삭제한다.
            Bullet bullet = collision.collider.GetComponent<Bullet>(); //GetComponent
            if (bullet.BType == BulletType.Main) //enum
            {
                Health -= 2;
            }
            else if(bullet.BType == BulletType.Sub)
            {
                Health -= 1;
            }

            // Destroy(collision.collider.gameObject);
            // 총알 삭제
            collision.collider.gameObject.SetActive(false);

            // 적의 체력이 적다면..
            if (Health <= 0)
            {
                Death();
                MakeItem();
            }
            else
            {
                    MyAnimator.Play("Hit");
            }

            
        }
}
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 중일 때 매번
        //Debug.Log("Stay");

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌이 끝났을 때
        //Debug.Log("Exit");
    }

    public void Death() //Death() 함수
    {
        //나죽자
        gameObject.SetActive(false);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;

        // 목표: 스코어를 증가시키고 싶다.
        //1. 씬에서  ScoreManager 게임 오브젝트를 찾아온다.
        // GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager 게임 오브젝트에서 ScoreManager 스크립트 컴포넌트를 얻어온다.
        // ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. 컴포넌트의 Score 속성을 증가시킨다. 
        // int score = scoreManager.GetScore();
        // scoreManager.SetScore(score + 1);
        //Debug.Log(scoreManager.GetScore());

        // 싱글톤 객체 참조로 변경
        //ScoreManager.Instance.AddScore();
        ScoreManager.Instance.Score += 1; //여기서 더하기 1
        
        
        
        
    }

    public void MakeItem() //MakeItem() 함수
    {
        // 목표: 50% 확률로 체력 올려주는 아이템, 50% 확률로 이동속도 올려주는 아이템 (확률넣기)
        if (Random.Range(0, 2) == 0)
        {
            // -체력을 올려주는 아이템 만들고
            GameObject item_Health = Instantiate(ItemPrefab_Health);
            // -위치를 나의 위치로 수정
            item_Health.transform.position = this.transform.position;
        }
        else
        {
            // -이동속도 올려주는 아이템 만들고
            GameObject item_Speed = Instantiate(ItemPrefab_Speed);
            // -위치를 나의 위치로 수정
            item_Speed.transform.position = this.transform.position;
        }
    }
}
