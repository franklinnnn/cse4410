using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider: MonoBehaviour
{
    public SpriteRenderer parentRenderer;
    public float attack;

    void Start()
    {

    }

    void Update()
    {
        transform.localScale = new Vector3(parentRenderer.flipX ? -1 : 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(attack);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(attack);
        }
    }
}
