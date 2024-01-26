using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    /*
        목표: 플레이어를 이동하고 싶다.
        필요 속성:
        - 이동 속도
        순서: 
        1. 키보드 입력을 받는다.
        2. 키보드 입력에 따라 이동할 방향을 계산한다.
        3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
     */

    public float Speed = 3f; // 이동 속도: 초당 3unit만큼 이동하겠다. **

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
        //유니티 Transform 에 있는 걸 translate(이동,옮기다)시킨다. 
        // (0,1) * 3 -> (0,3) * Time.deltaTime
        // deltaTime은 프레임 간 시간 간격을 반환한다.
        // 30fps: d-> 0.03초
        // 60fps: d-> 0.016초
        //transform.Rotate(Vector2.left);
        //transform.Scale()

        // 1.키보드 입력을 받는다.
        // float h = Input.GetAxis("Horizontal"); // Horizontal: 수평의 // -1.0f ~ 0f ~ +1.0f // Input: 입력키
        // float v = Input.GetAxis("Vertical");// 수직 입력값을 받아온다. //axis: 중심선
        float h = Input.GetAxisRaw("Horizontal"); // Horizontal: 수평의 // -1.0f, 0f, +1.0f // Input: 입력키
        float v = Input.GetAxisRaw("Vertical");// 수직 입력값을 받아온다. //raw: 날것의(가속도x)
        /// Debug.Log($"h:{h}, v: {v}");

        // 애니메이터에게 파리미터(매개변수) 값을 넘겨준다. 
        MyAnimator.SetInteger("h", (int)h);



        // 2.키보드 입력에 따라 이동할 방향을 계산한다.
        //Vector2 dir = Vector2.right * h + Vector2.up * v;
        // (1,0) * h + (0,1) * v = (h, v)
        //방향을 각 성분으로 제작
       Vector2 dir = new Vector2(h, v); /// Debug.Log($"정규화 전: {dir.magnitude}"); // 정규화라는 개념 중요* (게임)
                                         // 이동 방향을 정규화(방향은 같지만, 길이를 1로 만들어줌)

        dir = dir.normalized;
        /// Debug.Log($"정규화 후: {dir.magnitude}");


        // 3.이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
        // Debug.Log(Time.deltaTime);
        // transform.Translate( dir * Speed * Time.deltaTime);
        // 공식을 이용한 이동 
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;


        //newPosition.y = Mathf.Max(MinY, newPosition.y);
        //newPosition.y = Mathf.Min(newPosition.y, MaxY);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY); //Clamp : 값의 범위 한정

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
        transform.position = newPosition; // 플레이어의 위치 = 새로운 위치


        // 4. 현재 위치 출력
        /// Debug.Log(transform.position);
        // transform.position = new Vector2(0,1); // 고정됨
    }


    private void SpeedUpDown()
    {
        //목표: Q/E 버튼을 누르면 속력을 바꾸고 싶다.
        // 속성
        //-속력 (Speed)
        //순서:

        // 1. Q/E 버튼 입력을 판단한다.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 2. Q버튼이 눌렸다면 스피드 1다운
            Speed--;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 3. E버튼이 눌렸다면 스피드 1업
            Speed++;
        }
    }

        
}

