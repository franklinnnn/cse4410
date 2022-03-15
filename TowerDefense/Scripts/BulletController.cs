using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    public float speed;
    Rigidbody2D rb2dBullet;
    public float damage;

    private void Awake() 
    {
        rb2dBullet = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable() 
    {
        rb2dBullet.AddForce(transform.up * speed);
        Invoke("Disable", 4f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Disable()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
            Invoke("Disable", 0.1f);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
