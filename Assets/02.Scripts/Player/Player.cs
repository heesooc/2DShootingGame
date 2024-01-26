using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int Health = 3;

    public AudioSource HitSource;

    private void Start()
    {
        /*// GetComponent<컴포넌트 타입 > (); -> 게임 오브젝트의 컴포넌트를 가져오는 메서드
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;

        //Transform tr = GetComponent<Transform>();
        //tr.position = new Vector2(0f, -2.7f);
        transform.position = new Vector2(0f, -2.7f);

        PlayerMove playerMove = GetComponent<PlayerMove>();
        playerMove.Speed = 5f;
       */

    }

    public void OnCollosionEnter2D(Collider2D collision)
    {
            HitSource.Play();
        //todo: 사운드 (기록해두어서 todo키워드로 금방 찾기)
        
    }

    public int GetHealth()
    {
        return Health;
    }

    public void SetHealth(int health)
    {
        Health = health;
    }

    public void AddHealth(int health)
    {
        Health += health;
    }
}
