using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    
    Rigidbody2D rb2dBall;
    public float speed;
    public float randomUp;

    Vector3 startPosition;

    GameController cont;

    // Start is called before the first frame update
    void Start()
    {
        rb2dBall = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        cont = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        Invoke("PushBall", 1f);
    }

    void PushBall()
    {
        int direction = Random.Range(0, 2); // random integer from 0 to 1
        float x;
        if(direction == 0) // go right/up
        {
            x = speed;
        }
        else
        {
            x = -speed; // go left/down
        }

        rb2dBall.AddForce(new Vector2(x, -randomUp));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision gameObject: " + collision.gameObject.name);
        Debug.Log("gameObject: " + gameObject.name);

        if(collision.gameObject.CompareTag("Player"))
        {
            Vector2 velocity;
            velocity.y = rb2dBall.velocity.y;
            velocity.x = (rb2dBall.velocity.x / 2) + (collision.collider.attachedRigidbody.velocity.x / 3);
            rb2dBall.velocity = velocity;
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            Vector2 velocity;
            velocity.y = rb2dBall.velocity.y;
            velocity.x = (rb2dBall.velocity.x / 2) + (collision.collider.attachedRigidbody.velocity.x / 3);
            rb2dBall.velocity = velocity;
        }

        if(collision.gameObject.CompareTag("Brick"))
        {
            cont.HitBrick();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Goal"))
        {
            cont.LoseLife();
            rb2dBall.velocity = Vector2.zero;
            transform.position = startPosition;
            Invoke("PushBall", 2f);
        }
    }
}
