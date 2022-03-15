using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb2dEnemy;
    public float speed;
    public float maxHealth;
    [SerializeField]
    float health;

    Transform target;

    [SerializeField]
    int currentWaypoint;
    GameController controller;
    public float rotationSpeed;

    float distance;

    bool canMove = true;

    public float damage;

    public float dropCredits;

    public GameObject explosion;

    void Awake() 
    {
        rb2dEnemy = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
        canMove = true;
    }

    private void OnEnable() 
    {
        health = maxHealth;
        currentWaypoint = 0;
        target = controller.waypoints[currentWaypoint];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);

        if(canMove)
        {
            rb2dEnemy.AddForce(transform.up * speed * Time.deltaTime);
        }

        distance = Vector2.Distance(transform.position, target.position);
        if(distance <= 0.01f)
        {
            canMove = false;
            Invoke("CanMove", 0.5f);
            if(currentWaypoint < controller.waypoints.Length - 1) // if current waypoint is not last waypoint
            {
                currentWaypoint++;
                target = controller.waypoints[currentWaypoint];
            }
            else // waypoint is last waypoint
            {
                controller.TakeDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }

    void CanMove()
    {
        canMove = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            controller.GiveMoney(dropCredits);
            Instantiate(explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
