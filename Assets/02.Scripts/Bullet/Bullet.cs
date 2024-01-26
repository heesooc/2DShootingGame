using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType // �Ѿ� Ÿ�Կ� ���� ������(����� ����ϱ� ���� �ϳ��� �̸����� �׷�ȭ�ϴ� ��)
{
    Main = 0,
    Sub = 1,
    Pet = 2
}

public class Bullet : MonoBehaviour 
{
    //public int BulletType=0; // 0�̸� ���Ѿ�, 1�̸� �����Ѿ�, 2�� ���� ��� �Ѿ�
    public BulletType BType;
    

    // ��ǥ : �Ѿ��� ���� ��� �̵��ϰ� �ʹ�.
    // �Ӽ�:
    // - �ӷ�
    // ���� ����
    // 1. �̵��� ������ ���Ѵ�.
    // 2. �̵��Ѵ�. 

    public float Speed = 5f;

    public const float MinX = -3f;
    public const float MaxX = 3f;

    void Update()
    {
        // 1. �̵��� ������ ���Ѵ�.
        Vector2 dir = Vector2.up;

        // 2. �̵��Ѵ�. 
        //transform.Translate(dir * Speed * Time.deltaTime);
        // ���ο� ��ġ = ���� ��ġ + �ӵ� * �ð�
        transform.position +=  (Vector3)(dir* Speed) * Time.deltaTime;
    }

}
