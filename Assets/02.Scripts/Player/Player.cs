using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 3;

    public AudioSource HitSource;

    private void Start()
    {
        /*// GetComponent<������Ʈ Ÿ�� > (); -> ���� ������Ʈ�� ������Ʈ�� �������� �޼���
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
        //todo: ���� (����صξ todoŰ����� �ݹ� ã��)
        
    }
}
