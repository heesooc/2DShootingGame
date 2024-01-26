using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float Speed=1f;

    private void Update()
    {
        // ��� ��ũ��
        // -> ���� ȭ�鿡�� ��� �̹����� ������ �ӵ���
        // ������ ĳ���ͳ� ���� ���� �������� �� �������� ������ִ� ���
        // (ĳ���ʹ� �״�� �ΰ� ��游 �������� ĳ���Ͱ� �����̴� ��ó�� �������� �Ѵ�. )

        // ��ǥ: �Ʒ��� �̵��ϰ� �ʹ�.
        // ����:
        // 1. ������ ���ϰ�
        Vector2 dir = Vector2.down;

        // 2. �̵��Ѵ�. 
        // ���ο� ��ġ = ���� ��ġ + �ӵ� * �ð�
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;

        if(newPosition.y < -12.6)
        {
            newPosition.y = 12.6f;
        }
        transform.position = newPosition;
    }
}
