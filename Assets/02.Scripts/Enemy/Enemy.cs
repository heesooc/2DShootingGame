using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType // �� Ÿ�� '������'
{
    Basic, // EnemyType.Basic Ÿ��: �Ʒ��� �̵�,
    Target,  // EnemyType.Target Ÿ��: 'ó��' �¾�� �� �÷��̾ �ִ� �������� �̵�
    Follow
}
public class Enemy : MonoBehaviour
{
    public int Health = 2;

    public GameObject ItemPrefab_Health;
    public GameObject ItemPrefab_Speed;

    // ��ǥ: ���� �Ʒ��� �̵���Ű�� �ʹ�.
    // �Ӽ�:
    // - �ӷ�

    public float Speed = 3f; // �̵� �ӵ�: �ʴ� 3unit��ŭ �̵��ϰڴ�.

    public const float MinX = -3f;
    public const float MaxX = 3f;

    /***/
    public EnemyType EType; // Basic vs. Target Ÿ��
    public Vector2 _dir; //���� ������
    private GameObject _target;


    public Animator MyAnimator;

    public GameObject ExplosionVFXPrefab;//����


    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();

    }

    /*
    // ��ǥ:
    // EnemyType.Basic Ÿ��: �Ʒ��� �̵�,
    // EnemyType.Target Ÿ��: ó�� �¾�� �� �÷��̾ �ִ� �������� �̵�
    // �Ӽ�
    // - EnemyType Ÿ��
    // ���� ����:
    // 1. ������ �� ������ ���Ѵ�. (�÷��̾ �ִ� ����)
    // 2. ������ ���� �̵��Ѵ�.
    */


    // ������ ��
    void Start()
    {
        // ĳ��: ���� ������ �����͸� �� ����� ��ҿ� �����صΰ� �ʿ��� �� ������ ���� �� 
        // ������ �� �÷��̾ ã�Ƽ� ����صд�. 
        _target = GameObject.Find("Player");

        ///MyAnimator 

        if (EType == EnemyType.Target)
        {
            // 1. ������ �� ������ ���Ѵ�. (�÷��̾ �ִ� ����)
            // 1-1. �÷��̾ 'ã�´�'.
            //GameObject.FindGameObjectWithTag("Player");


            // 1-2. ������ '���Ѵ�'.
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize(); // ������ ũ�⸦ 1�� �����. �ذ� �̸��� ����, �������Ѵ�. 


            // 1. ������ ���Ѵ�. 
            //tan�� = y/x    ->  �� = y/x * atan(ź��Ʈ�� ���Լ�)
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            Debug.Log(radian); // 'ȣ'���� -> ���� ��
            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. ������ �°� ȸ���Ѵ�.
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree+90)); //�̹��� ���ҽ��� ���� 90�� ������
                                                                                 // transform.LookAt(_target.transform); <-3d���� ��. ��ȣx
            transform.eulerAngles = new Vector3(0, 0, degree + 90);
        }

        else // Basic Ÿ��
        {
            _dir = Vector2.down;
        }

    }
    
    void Update()
    {
        //���� ����
        // 2. �̵��Ѵ�. 
        // ���ο� ��ġ = ���� ��ġ + �ӵ� * �ð�
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;

        //10 % Ȯ���� ���� �� ������� Follow�� �� �����ϱ�
        if (EType == EnemyType.Follow)
        {
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();

            // 1. ������ ���Ѵ�. 
            //tan�� = y/x    ->  �� = y/x * atan(ź��Ʈ�� ���Լ�)
            float radian = Mathf.Atan2(_dir.y, _dir.x); // 'ȣ'���� -> ���� ��
            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. ������ �°� ȸ���Ѵ�.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); //�̹��� ���ҽ��� ���� 90�� ������
                                                                                  
        }


        // �ִϸ����Ϳ��� �ĸ�����(�Ű�����) ���� �Ѱ��ش�. 
        // MyAnimator.SetInteger("h", (int)_dir); (x)

    }

    /*
    // ��ǥ: �浹�ϸ� ���� �÷��̾ �����ϰ� �ʹ�.
    // ���� ����:
    // 1. ���࿡ �浹�� �Ͼ��
    // 2. ���� �÷��̾ �����Ѵ�.
    */

    // �浹�� �Ͼ�� ȣ��Ǵ� �̺�Ʈ �Լ�
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 1.�浹�� �������� ��
        Debug.Log("Enter");
        

        // �浹�� ���� ������Ʈ�� �±׸� Ȯ��
        Debug.Log(collision.collider.tag); // <- Player or Bullet


        // 2.���� �÷��̾ ����
        // ������(�� �ڽ�)
        //Destroy(this.gameObject); //enemy ����������, �����Ѿ˿��� 2������ �׾����
        
        // �÷��̾�� ���� 3�� �浹�ϸ� �װ� �����
        if (collision.collider.tag == "Player")
        {
            Death();

            // �÷��̾� ��ũ��Ʈ�� �����´�.
            Player player = collision.collider.GetComponent<Player>();
            // �÷��̾� ü���� -= 1
            player.Health -= 1;

            
           // �÷��̾� ü���� ���ٸ�..
            if (player.Health == 0)
            {
                Destroy(collision.collider.gameObject);
            }

        }
        else if (collision.collider.tag == "Bullet") //Tag //enemy�� �Ѿ��� �ε����� ��
        {
            // 2. �浹�� enemy�� �����Ѵ�.
            Bullet bullet = collision.collider.GetComponent<Bullet>(); //GetComponent
            if (bullet.BType == BulletType.Main) //enum
            {
                Health -= 2;
            }
            else if(bullet.BType == BulletType.Sub)
            {
                Health -= 1;
            }

            Destroy(collision.collider.gameObject);


            // ���� ü���� ���ٸ�..
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
        // �浹 ���� �� �Ź�
        Debug.Log("Stay");

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹�� ������ ��
        Debug.Log("Exit");
    }

    public void Death() //Death() �Լ�
    {
        //������
        Destroy(this.gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;

        // ��ǥ: ���ھ ������Ű�� �ʹ�.
        //1. ������  ScoreManager ���� ������Ʈ�� ã�ƿ´�.
        GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager ���� ������Ʈ���� ScoreManager ��ũ��Ʈ ������Ʈ�� ���´�.
        ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. ������Ʈ�� Score �Ӽ��� ������Ų��. 
        scoreManager.Score += 1;
        Debug.Log(scoreManager.Score);

        // ��ǥ: ���ھ ȭ�鿡 ǥ���Ѵ�.
        scoreManager.ScoreTextUI.text = $"����: {scoreManager.Score}";

        // ��ǥ: �ְ� ������ �����ϰ� UI�� ǥ���ϰ� �ʹ�. 
        // 1. ���࿡ ���� ������ �ְ� �������� ũ�ٸ�
        if(scoreManager.Score > scoreManager.BestScore) 
        {
            // 2. �ְ� ������ �����ϰ�,
            scoreManager.BestScore = scoreManager.Score;

            // ��ǥ: �ְ� ������ ����
            // 'PlayerPrefs' Ŭ������ ��� //Prefs ��: ȯ�漳��
            // -> �����͸� 'Ű(Key)'�� '��(Value)'�� ���·� �����ϴ� Ŭ����
            // ������ �� �ִ� ������ Ÿ��: int, float, string
            // Ÿ�Ժ��� ����/�ε尡 ������ Set/Get �޼��尡 �ִ�.
            PlayerPrefs.SetInt("BestScore", scoreManager.BestScore);

            // 3. UI�� ǥ���Ѵ�.
            scoreManager.BestScoreTextUI.text = $"�ְ� ����: {scoreManager.Score}";
        }
    }

    public void MakeItem() //MakeItem() �Լ�
    {
        // ��ǥ: 50% Ȯ���� ü�� �÷��ִ� ������, 50% Ȯ���� �̵��ӵ� �÷��ִ� ������ (Ȯ���ֱ�)
        if (Random.Range(0, 2) == 0)
        {
            // -ü���� �÷��ִ� ������ �����
            GameObject item_Health = Instantiate(ItemPrefab_Health);
            // -��ġ�� ���� ��ġ�� ����
            item_Health.transform.position = this.transform.position;
        }
        else
        {
            // -�̵��ӵ� �÷��ִ� ������ �����
            GameObject item_Speed = Instantiate(ItemPrefab_Speed);
            // -��ġ�� ���� ��ġ�� ����
            item_Speed.transform.position = this.transform.position;
        }
    }
}
