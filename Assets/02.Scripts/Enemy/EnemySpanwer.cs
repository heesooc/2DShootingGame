using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    // 역할: 일정시간마다 적을 프리팹으로부터 생성해서 내 위치에 갖다 놓고 싶다.
    // 필요 속성
    // - 적 프리팹
    // - 일정시간
    // - 현재시간
    

    // 구현 순서:
    // 1. 시간이 흐르다가
    // 2. 만약에 시간이 일정시간이 되면
    // 3. 프리팹으로부터 적을 생성한다.
    // 4. 생성한 적의 위치를 내 위치로 바꾼다.

    [Header("적 프리팹")]
    public GameObject EnemyPrefab; //적 프리팹
    public GameObject EnemyPrefabTarget;
    public GameObject EnemyFollow;

    // 풀사이즈: 15 (15 *3 = 45)
    public int PoolSize;
    // 풀(창고)
    public List<Enemy> EnemyPool; 


    [Header("타이머")]
    public float SpawnTime; //일정시간
    public float Timer; //현재시간

    
    // 목표: 적 생성 시간을 랜덤하게 하고 싶다.
    // 필요 속성:
    // - 최소 시간
    // - 최대 시간
    public float MinTime = 0.5f;
    public float MaxTime = 1.5f;

    public void Awake()
    {
        EnemyPool = new List<Enemy>();

        // (생성 -> 끄고 -> 넣는다) * PoolSize(15).
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyObject = Instantiate(EnemyPrefab); // 베이직 생성
            enemyObject.SetActive(false);
            EnemyPool.Add(enemyObject.GetComponent<Enemy>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyObject = Instantiate(EnemyPrefabTarget); // 베이직 생성
            enemyObject.SetActive(false);
            EnemyPool.Add(enemyObject.GetComponent<Enemy>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyObject = Instantiate(EnemyFollow); // 베이직 생성
            enemyObject.SetActive(false);
            EnemyPool.Add(enemyObject.GetComponent<Enemy>());
        }

    }

    void SetRandomTime()
    {
        // 시작할 때 적 생성 시간을 랜덤하게 설정한다.
        SpawnTime = Random.Range(MinTime, MaxTime);

        Timer = SpawnTime;
    }


    void Start()
    {
        SetRandomTime();
    }

    void Update()
    {
        // 타이머 계산
        Timer -= Time.deltaTime;

        //1. 타이머가 0보다 작은 상태에서 발사 버튼을 누르면
        if (Timer <= 0 )
        {
            //30% 확률로 Target형, 나머지 확률(70%) Basic형 적 생성하게 하기
            Enemy enemy = null; //오브젝트 탄생.
            int randomNumber = Random.Range(0, 10); // 0,1,2.3,4,5,6,7,8,9

            // 2. 위치를 설정한다.


            //10 % 확률로 적이 날 따라오는 Follow형 적 생성하기
            if (randomNumber < 1)
            {
                
                foreach(Enemy e in EnemyPool)
                {
                    if(!e.gameObject.activeInHierarchy && e.EType == EnemyType.Follow)
                    {
                        enemy = e;

                        break;
                    }
                }
            }

            if (randomNumber < 3)
            {
                foreach (Enemy e in EnemyPool)
                {
                    if (!e.gameObject.activeInHierarchy && e.EType == EnemyType.Target)
                    {
                        enemy = e;

                        break;
                    }
                }
            }
            else
            {
                foreach (Enemy e in EnemyPool)
                {
                    if (!e.gameObject.activeInHierarchy && e.EType == EnemyType.Basic)
                    {
                        enemy = e;

                        break;
                    }
                }
            }

            enemy.gameObject.SetActive(true);

            enemy.transform.position = this.transform.position;

            SetRandomTime();
        }
    }
}
