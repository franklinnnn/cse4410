using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    Rigidbody2D rb2dEnemy;
    PlayerController player;
    public float xSpeed, ySpeed;

    public GameObject bullet;
    public float timeBetweenAttackLow = 0.5f;
    public float timeBetweenAttackHigh = 2f;

    float attackCoolDown;

    public int maxEnemyHealth;
    int enemyHealth;

    GameController controller;
    public int amount;

    Vector2 bounds;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb2dEnemy = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        attackCoolDown = Random.Range(timeBetweenAttackLow, timeBetweenAttackHigh);
        enemyHealth = maxEnemyHealth;
        controller = FindObjectOfType<GameController>();
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0f;
        if(player != null)
        {
            if(player.transform.position.x > transform.position.x) // enemy to left of player
                x = xSpeed;
            else if(player.transform.position.x < transform.position.x) // enemy to right of player
                x = -xSpeed;
        }
        rb2dEnemy.AddForce(new Vector2(x, -ySpeed) * Time.deltaTime); 

        if(attackCoolDown > 0)
        {
            attackCoolDown -= Time.deltaTime;
        }
        else
        {
            Attack();
        }

        if(transform.position.y < -bounds.y)
        {
            controller.AddScore(-amount);
            Destroy(gameObject);
        }

    }

    void Attack()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        attackCoolDown = Random.Range(timeBetweenAttackLow, timeBetweenAttackHigh);
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            Die();
        }
    }
     void Die()
     {
        controller.AddScore(amount);
        Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
     }
}
