using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    // ����: �����ð����� ���� ���������κ��� �����ؼ� �� ��ġ�� ���� ���� �ʹ�.
    // �ʿ� �Ӽ�
    // - �� ������
    // - �����ð�
    // - ����ð�
    

    // ���� ����:
    // 1. �ð��� �帣�ٰ�
    // 2. ���࿡ �ð��� �����ð��� �Ǹ�
    // 3. ���������κ��� ���� �����Ѵ�.
    // 4. ������ ���� ��ġ�� �� ��ġ�� �ٲ۴�.

    [Header("�� ������")]
    public GameObject EnemyPrefab; //�� ������
    public GameObject EnemyPrefabTarget;
    public GameObject EnemyFollow;

    [Header("Ÿ�̸�")]
    public float SpawnTime; //�����ð�
    public float Timer; //����ð�

    
    // ��ǥ: �� ���� �ð��� �����ϰ� �ϰ� �ʹ�.
    // �ʿ� �Ӽ�:
    // - �ּ� �ð�
    // - �ִ� �ð�
    public float MinTime = 0.5f;
    public float MaxTime = 1.5f;
    
    void SetRandomTime()
    {
        // ������ �� �� ���� �ð��� �����ϰ� �����Ѵ�.
        SpawnTime = Random.Range(MinTime, MaxTime);

        Timer = SpawnTime;
    }


    void Start()
    {
        SetRandomTime();
    }

    void Update()
    {
        // Ÿ�̸� ���
        Timer -= Time.deltaTime;

        //1. Ÿ�̸Ӱ� 0���� ���� ���¿��� �߻� ��ư�� ������
        if (Timer <= 0 )
        {
            //30% Ȯ���� Target��, ������ Ȯ��(70%) Basic�� �� �����ϰ� �ϱ�
            GameObject enemy = null; //������Ʈ ź��.
            int randomNumber = Random.Range(0, 10); // 0,1,2.3,4,5,6,7,8,9
            
            //10 % Ȯ���� ���� �� ������� Follow�� �� �����ϱ�
            if (randomNumber < 1)
            {
                enemy = Instantiate(EnemyFollow);
            }

            if (randomNumber < 3)
            {
                enemy = Instantiate(EnemyPrefabTarget);
            }
            else
            {
                enemy = Instantiate(EnemyPrefab);
            }

                // 2. ��ġ�� �����Ѵ�.
                enemy.transform.position = this.transform.position;

            SetRandomTime();
        }
    }
}
