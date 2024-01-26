using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Item : MonoBehaviour
{
    private const float FOLLOW_TIME = 3F;

    public float _timer = 0f; // 시간을 체크할 변수

    public int MyType = 0; // 0: 체력을 올려주는 타입, 1: 스피드를 올려주는 타입

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
            // 1. 플레이어 게임오브젝트를 찾고
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            // 2. 방향을 정하고
            Vector3 dir = target.transform.position - this.transform.position;
            dir.Normalize();
            // 3. 스피드에 맞게 이동
            Vector3 newPosition = transform.position + dir * 10f * Time.deltaTime;
            this.transform.position = newPosition;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter!");
    }

    // (다른 콜라이더에 의해) 트리거가 발동될 때
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("트리거 시작!");
        
            //Destroy(this.gameObject); //Layer, isTrigger 이용하면 if(player만났을 때) 조건문 쓸 필요 없음
    }

    // (다른 콜라이더에 의해) 트리거가 발동 중일 때
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        _timer += Time.deltaTime;
        if ( _timer >= 1.0) 
        {
            if ( MyType == 0) // 0: 체력을 올려주는 타입
            {
                Player player = otherCollider.gameObject.GetComponent<Player>();
                player.Health++;
                Debug.Log($"현재 플레이어의 체력:{player.Health}");
            }

            else if ( MyType == 1) // 1: 스피드를 올려주는 타입
            {
                // 타입이 1이면 플레이어의 스피드 올려주기
                PlayerMove playerMove = otherCollider.gameObject.GetComponent<PlayerMove>();
                playerMove.Speed += 1;
            }

            Destroy(this.gameObject);
            GameObject vfx = Instantiate(EatVFXPrefab);
            vfx.transform.position = otherCollider.transform.position;

        }

            Debug.Log("트리거 중!");
    }

    // (다른 콜라이더에 의해) 트리거가 끝났을 때
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        _timer = 0.0f;
        Debug.Log("트리거 종료!");
    }
}
 