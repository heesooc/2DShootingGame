using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType // 총알 타입에 대한 열거형(상수를 기억하기 좋게 하나의 이름으로 그룹화하는 것)
{
    Main = 0,
    Sub = 1,
    Pet = 2
}

public class Bullet : MonoBehaviour 
{
    //public int BulletType=0; // 0이면 주총알, 1이면 보조총알, 2면 펫이 쏘는 총알
    public BulletType BType;
    

    // 목표 : 총알이 위로 계속 이동하고 싶다.
    // 속성:
    // - 속력
    // 구현 순서
    // 1. 이동할 방향을 구한다.
    // 2. 이동한다. 

    public float Speed = 5f;

    public const float MinX = -3f;
    public const float MaxX = 3f;

    void Update()
    {
        // 1. 이동할 방향을 구한다.
        Vector2 dir = Vector2.up;

        // 2. 이동한다. 
        //transform.Translate(dir * Speed * Time.deltaTime);
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        transform.position +=  (Vector3)(dir* Speed) * Time.deltaTime;
    }

}
