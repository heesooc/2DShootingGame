using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    // �̺�Ʈ �Լ��� ���� ����
    private void Awake()
    {
        // �ν��Ͻ�ȭ �� ���Ŀ� ȣ�� // �ν��Ͻ�ȭ: ������ �ʵ��� ���� ���� �� ���� ���� ������ �аų� �����ϴ� ���μ���
        // �ַ� ������ ���³� ������ �ʱ�ȭ�� �� ���
        Debug.Log("Awake");
    }

    private void OnEnable()
    {
        // ��� ������ ������! ȣ��ȴ�.
        // ����ڰ� ���� �̺�Ʈ�� ������ �� 
        Debug.Log("OnEnable");
    }

    private void Start()
    {
        // ������ �� ȣ��ȴ�.
        // �ٸ� ��ũ��Ʈ�� ��� Awake ��� ����� ���Ŀ� ȣ��ȴ�.
        Debug.Log("Start");
    }

    // Input ������Ʈ

    private void Update()
    {
        // �� �����Ӹ��� ȣ��ȴ�.     
    }

    // �ڷ�ƾ ������Ʈ //�ڷ�ƾ : Co(�Բ�, ����) + routine(��Ģ�� ���� ����, �۾��� ����) 2���� ������ �ܾ�, �Բ� �����ϸ� ��Ģ�� �ִ� ���� ����

    private void LateUpdate()
    {
        // ��� Ȱ��ȭ�� ��ũ��Ʈ�� Update �Լ��� ȣ��ǰ� ���� �� ���� ȣ��ȴ�. 
    }

    private void OnDisable()
    {
        // ��� �Ұ����� ������! ȣ��ȴ�.
        // ����ڰ� ���� �̺�Ʈ�� ���� ���� �� �� 
        Debug.Log("OnDisable");
    }
}
