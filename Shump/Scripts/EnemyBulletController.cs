using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    Rigidbody2D rb2dBullet;
    public float speed;
    public int damage = 1;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb2dBullet = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb2dBullet.AddForce(-Vector2.up * speed);
        Invoke("Disable", 5f);
    }

    void Disable()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Invoke("Disable", 0.01f);
        }
    }
}
