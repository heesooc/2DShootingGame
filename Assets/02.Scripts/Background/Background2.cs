using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2 : MonoBehaviour
{
    // ��ǥ: ���͸����� ������(Offset)�� �̿��ؼ� ��� ��ũ���� �ǵ��� �ϰ� �ʹ�.
    // �ʿ� �Ӽ�
    // - ���͸���
    // - ��ũ�� �ӵ�
    public Material MyMaterial;
    public float ScrollSpeed = 0.2f;

    // ���� ����
    // 0. �� �����Ӹ��� (Update())
    private void Update()
    {
        // 1. ������ ���Ѵ�.
        Vector2 dir = Vector2.up;

        // 2. (�������� �����ؼ�) ��ũ���� �Ѵ�. 
        MyMaterial.mainTextureOffset += dir * ScrollSpeed * Time.deltaTime;
    }
}
