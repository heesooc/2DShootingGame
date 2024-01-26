using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Item : MonoBehaviour
{
    private const float FOLLOW_TIME = 3F;

    public float _timer = 0f; // �ð��� üũ�� ����

    public int MyType = 0; // 0: ü���� �÷��ִ� Ÿ��, 1: ���ǵ带 �÷��ִ� Ÿ��

    public Animator MyAnimator;

    public GameObject EatVFXPrefab;

    public void Start()
    {
        _timer = 0f;

        MyAnimator = GetComponent<Animator>();

        MyAnimator.SetInteger("ItemType", MyType);
    }
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= FOLLOW_TIME)
        {
            // 1. �÷��̾� ���ӿ�����Ʈ�� ã��
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            // 2. ������ ���ϰ�
            Vector3 dir = target.transform.position - this.transform.position;
            dir.Normalize();
            // 3. ���ǵ忡 �°� �̵�
            Vector3 newPosition = transform.position + dir * 10f * Time.deltaTime;
            this.transform.position = newPosition;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter!");
    }

    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ��� ��
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("Ʈ���� ����!");
        
            //Destroy(this.gameObject); //Layer, isTrigger �̿��ϸ� if(player������ ��) ���ǹ� �� �ʿ� ����
    }

    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ� ���� ��
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        _timer += Time.deltaTime;
        if ( _timer >= 1.0) 
        {
            if ( MyType == 0) // 0: ü���� �÷��ִ� Ÿ��
            {
                Player player = otherCollider.gameObject.GetComponent<Player>();
                player.Health++;
                Debug.Log($"���� �÷��̾��� ü��:{player.Health}");
            }

            else if ( MyType == 1) // 1: ���ǵ带 �÷��ִ� Ÿ��
            {
                // Ÿ���� 1�̸� �÷��̾��� ���ǵ� �÷��ֱ�
                PlayerMove playerMove = otherCollider.gameObject.GetComponent<PlayerMove>();
                playerMove.Speed += 1;
            }

            Destroy(this.gameObject);
            GameObject vfx = Instantiate(EatVFXPrefab);
            vfx.transform.position = otherCollider.transform.position;

        }

            Debug.Log("Ʈ���� ��!");
    }

    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� ������ ��
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        _timer = 0.0f;
        Debug.Log("Ʈ���� ����!");
    }
}
 