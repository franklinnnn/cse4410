using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHealth;
    protected float health;
    public Image healthImage;

    public float speed;
    public float runSpeed;
    public float chaseRange;
    public float attackRange;
    public enum enemystates { move, chase, attack }
    public enemystates currentState = enemystates.move;
    protected Rigidbody2D enemyRigidbody;

    public LayerMask wallLayer;
    public float rayLength;

    public int direction; // 1 moves right, -1 moves left

    protected SpriteRenderer rend;

    protected float distance;
    protected PlayerController player;

    public float timeBetweenAttacks;
    protected float attackCooldown;
    protected Animator anim;

    private void OnEnable() 
    {
        health = maxHealth;
        direction = (Random.value >= 0.5f) ? 1 : -1;
        attackCooldown = timeBetweenAttacks;
    }

    void Awake() 
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
    }

    public virtual void Move()
    {

    }

     public virtual void Chase()
    {

    }

     public virtual void Attack()
    {

    }

     public virtual void Damage(float amount)
    {

    }

     public virtual void Die()
    {

    }
    // Update is called once per frame
    void Update()
    {
        rend.flipX = (direction == -1);
        switch(currentState)
        {
            case enemystates.move:
                Move();
                break;
            case enemystates.chase:
                Chase();
                break;
            case enemystates.attack:
                Attack();
                break;
                
        }

        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        healthImage.fillAmount = health / maxHealth;


    }
}

