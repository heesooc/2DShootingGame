using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private const float DEATH_TIME = 3F;
    private float _deathTimer = 0f;

    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy =enemies[i].GetComponent<Enemy>();
            enemy.Death();
            enemy.MakeItem();
        }
    }
    private void Update()
    {
        _deathTimer += Time.deltaTime;
        if (_deathTimer > DEATH_TIME)
        {
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();  
            if(enemy != null )
            {
                enemy.Death();
            }
        }
    }
}
