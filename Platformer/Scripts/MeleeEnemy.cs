using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    
    public override void Move()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, rayLength, wallLayer);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.right * direction - Vector2.up, rayLength, wallLayer);

        if(hit.collider != null)
        {
            direction *= -1;
        }

        if(hitDown.collider == null)
        {
            direction *= -1;
        }

        Debug.DrawRay(transform.position, Vector2.right * direction * rayLength);

        if(distance <= chaseRange)
        {
            currentState = enemystates.chase;
        }

        enemyRigidbody.AddForce(Vector2.right * direction * speed * Time.deltaTime);
    }

    public override void Chase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if(transform.position.x > player.transform.position.x) //player <- enemy
        {
            direction = -1; //enemy moves left
        }
        else
        {
            direction = 1;
        }

        if(distance >= chaseRange)
        {
            currentState = enemystates.move;
        }
        if(distance <= attackRange)
        {
            currentState = enemystates.attack;
        }

        enemyRigidbody.AddForce(Vector2.right * direction * runSpeed * Time.deltaTime);
    }

    public override void Attack()
    {
        if(attackCooldown < 0)
        {
            anim.SetBool("attack", true);
            Invoke("ResetAttack", 0.1f);
            attackCooldown = timeBetweenAttacks;
        }
        else
        {
            currentState = enemystates.chase;
        }
    }

    void ResetAttack()
    {
        anim.SetBool("attack", false);
    }

    public override void Damage(float amt)
    {
        health -= amt;
        if(health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }
}