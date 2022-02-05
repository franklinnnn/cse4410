using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    public float randomUp;

    Rigidbody2D rb2d;
    GameController controller;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        Invoke("PushBall", 1f); // enable script file after 1 second
    }

    void PushBall()
    {
        int direction = Random.Range(0, 2); // returns number from 0 to 1
        float x, y;
        if(direction == 0) // ball moves to the right
        {
            x = speed;
        }
        else // ball moves to the left
        {
            x = -speed;
        }
        y = Random.Range(-randomUp, randomUp);
        rb2d.AddForce(new Vector2(x, y));
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnColliderEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector2 velocity;
            velocity.x = rb2d.velocity.x;
            velocity.y = (rb2d.velocity.y / 2) + (collision.collider.attachedRigidbody.velocity.y) / 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Goal"))
        {
            if(rb2d.velocity.x > 0) // collide with right wall, left player scores
            {
                controller.Score(true);
            }
            else if(rb2d.velocity.x < 0) // collide with left wall, right player scores
            {
                controller.Score(false);
            }
            else
            {
                
            }

            rb2d.velocity = Vector2.zero;
            transform. position = Vector3.zero;

            Invoke("PushBall", 2f);
        }
    }
}
