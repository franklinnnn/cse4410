using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    Rigidbody2D rb2dBullet;
    public float speed;
    int damage = 1;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb2dBullet = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb2dBullet.AddForce(Vector2.up * speed);
        Invoke("Disable", 5f);
    }

    void Disable()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Invoke("Disable", 0.01f);
        }
    }
}
