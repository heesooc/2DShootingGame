using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //[�Ѿ� �߻� ����]
    // ��ǥ : �Ѿ��� ���� �߻��ϰ� �ʹ�.
    // �Ӽ�:
    // - �Ѿ�
    // - �ѱ� ��ġ
    // ���� ����
    //1. �߻� ��ư�� ������
    //2. ���������κ��� �Ѿ��� �������� �����,
    //3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.

    [Header("�Ѿ� ������")]
    public GameObject BulletPrefab; //�Ѿ� ������
    [Header("�����Ѿ� ������")]
    public GameObject SubBulletPrefab; //�Ѿ� ������

    [Header("�ѱ���")]
    public GameObject[] Muzzles; //�ѱ���
    public GameObject[] SubMuzzles;

    [Header("Ÿ�̸�")]
    public float Timer = 10f;
    public const float COOL_TIME = 0.6f;

    public float BoomTimer = 5f;
    public const float BOOM_COOL_TIME = 5f;

    [Header("�ڵ� ���")]
    public bool AutoMode = false;

    public AudioSource FireSource; // AudioSource: ����� Ŭ���� ��������ִ� ������Ʈ

    public GameObject SpecialMoveBoomPrefab; //�ʻ���
    private void CheckAutoMode()
    {
        // 1. 1/2 ��ư �Է��� �Ǵ��Ѵ�.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 2. 1�� Ű�� �ڵ� ���� ���
            Debug.Log("�ڵ� ���� ���");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 3. 2�� Ű�� ���� ���� ���
            Debug.Log("���� ���� ���");
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

        // Ÿ�̸� ���
        Timer -= Time.deltaTime;
        BoomTimer -= Time.deltaTime;

        //1. Ÿ�̸Ӱ� 0���� ���� ���¿��� �߻� ��ư�� ������
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            //todo: ���� (����صξ todoŰ����� �ݹ� ã��)
            FireSource.Play();

            // Ÿ�̸� �ʱ�ȭ
            Timer = COOL_TIME;
            
            //2. ���������κ��� �Ѿ��� �������� �����,
            //GameObject bullet = Instantiate(BulletPrefab); // Instantiate = �ν��Ͻ�ȭ(��üȭ ���) <- ���ο� �� �� �ϳ�.
            //GameObject bullet2 = Instantiate(BulletPrefab2);
            
            //3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
            //bullet.transform.position = Muzzle.transform.position;
            //bullet2.transform.position = Muzzle2.transform.position;

            // ��ǥ: �ѱ� ���� ��ŭ �Ѿ��� �����, ���� �Ѿ��� ��ġ�� �� �ѱ��� ��ġ�� �ٲ۴�.
            for (int i = 0; i < Muzzles.Length; i++)
            {
                // 1. �Ѿ��� �����
                GameObject bullet = Instantiate(BulletPrefab);
                GameObject bullet2 = Instantiate(SubBulletPrefab);

                // 2. ��ġ�� �����Ѵ�.
                bullet.transform.position = Muzzles[i].transform.position;
                bullet2.transform.position = SubMuzzles[i].transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && BoomTimer <= 0f)
        {
            // Ÿ�̸� �ʱ�ȭ
            BoomTimer = BOOM_COOL_TIME;
            // 3�� Ű�� �ʻ�� ���� ���
            GameObject vfx = Instantiate(SpecialMoveBoomPrefab);
            vfx.transform.position = new Vector2(0, 0);
        }
       // if (BoomTimer <= 2f)
        {
            //Destroy(SpecialMoveBoomPrefab);
        }
    }
}
