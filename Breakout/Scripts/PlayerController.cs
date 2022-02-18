using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2dPlayer;
    float input;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb2dPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Horizontal");
        rb2dPlayer.AddForce(Vector2.right * input * speed * Time.deltaTime);
    }
}
