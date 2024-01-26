using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    /*
        ��ǥ: �÷��̾ �̵��ϰ� �ʹ�.
        �ʿ� �Ӽ�:
        - �̵� �ӵ�
        ����: 
        1. Ű���� �Է��� �޴´�.
        2. Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
        3. �̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
     */

    public float Speed = 3f; // �̵� �ӵ�: �ʴ� 3unit��ŭ �̵��ϰڴ�. **

    public const float MinX = -3f;
    public const float MaxX = 3f;
    public const float MinY = -6f;
    public const float MaxY = 0f;

    public Animator MyAnimator;

    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();

    }

    void Update()
    {
  
        Move();

        SpeedUpDown();
    }

    private void Move()
    {
        //transform.Translate(Vector2.up * Speed * Time.deltaTime);
        //����Ƽ Transform �� �ִ� �� translate(�̵�,�ű��)��Ų��. 
        // (0,1) * 3 -> (0,3) * Time.deltaTime
        // deltaTime�� ������ �� �ð� ������ ��ȯ�Ѵ�.
        // 30fps: d-> 0.03��
        // 60fps: d-> 0.016��
        //transform.Rotate(Vector2.left);
        //transform.Scale()

        // 1.Ű���� �Է��� �޴´�.
        // float h = Input.GetAxis("Horizontal"); // Horizontal: ������ // -1.0f ~ 0f ~ +1.0f // Input: �Է�Ű
        // float v = Input.GetAxis("Vertical");// ���� �Է°��� �޾ƿ´�. //axis: �߽ɼ�
        float h = Input.GetAxisRaw("Horizontal"); // Horizontal: ������ // -1.0f, 0f, +1.0f // Input: �Է�Ű
        float v = Input.GetAxisRaw("Vertical");// ���� �Է°��� �޾ƿ´�. //raw: ������(���ӵ�x)
        /// Debug.Log($"h:{h}, v: {v}");

        // �ִϸ����Ϳ��� �ĸ�����(�Ű�����) ���� �Ѱ��ش�. 
        MyAnimator.SetInteger("h", (int)h);



        // 2.Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
        //Vector2 dir = Vector2.right * h + Vector2.up * v;
        // (1,0) * h + (0,1) * v = (h, v)
        //������ �� �������� ����
       Vector2 dir = new Vector2(h, v); /// Debug.Log($"����ȭ ��: {dir.magnitude}"); // ����ȭ��� ���� �߿�* (����)
                                         // �̵� ������ ����ȭ(������ ������, ���̸� 1�� �������)

        dir = dir.normalized;
        /// Debug.Log($"����ȭ ��: {dir.magnitude}");


        // 3.�̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
        // Debug.Log(Time.deltaTime);
        // transform.Translate( dir * Speed * Time.deltaTime);
        // ������ �̿��� �̵� 
        // ���ο� ��ġ = ���� ��ġ + �ӵ� * �ð�
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;


        //newPosition.y = Mathf.Max(MinY, newPosition.y);
        //newPosition.y = Mathf.Min(newPosition.y, MaxY);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY); //Clamp : ���� ���� ����

        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        /*
        if (newPosition.y > MaxY)
        { newPosition.y = MaxY; }
        else if (newPosition.y < MinY)
        { newPosition.y = MinY; }
        */
        /// Debug.Log($"x:{newPosition.x}, y:{newPosition.y}");
        //newPosition.x = 2;
        transform.position = newPosition; // �÷��̾��� ��ġ = ���ο� ��ġ


        // 4. ���� ��ġ ���
        /// Debug.Log(transform.position);
        // transform.position = new Vector2(0,1); // ������
    }


    private void SpeedUpDown()
    {
        //��ǥ: Q/E ��ư�� ������ �ӷ��� �ٲٰ� �ʹ�.
        // �Ӽ�
        //-�ӷ� (Speed)
        //����:

        // 1. Q/E ��ư �Է��� �Ǵ��Ѵ�.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 2. Q��ư�� ���ȴٸ� ���ǵ� 1�ٿ�
            Speed--;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 3. E��ư�� ���ȴٸ� ���ǵ� 1��
            Speed++;
        }
    }

        
}

