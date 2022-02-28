using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Starting states")]
    public float speed;
    Vector2 input;
    Rigidbody2D rb2dPlayer;

    [Header("Shooting")]
    public GameObject bullet;
    public GameObject[] bulletSpawnPositions;
    private float coolDown;
    public float timeBetweenShots;

    public GameObject flash;

    [Header("Health")]
    public int maxHealth = 10;
    public int health;
    public GameObject healthImage;
    public GameObject healthParent;
    public float timeBetweenDamage = 0.3f;
    float iframe;



    // Start is called before the first frame update
    void Start()
    {
        rb2dPlayer = GetComponent<Rigidbody2D>();
        coolDown = timeBetweenShots;
        health = maxHealth;
        iframe = timeBetweenDamage;
        for(int i = 0; i < health - 1; i++)
        {
            AddHeart();
        }
    }

    void AddHeart()
    {
        GameObject heart = Instantiate(healthImage);
        heart.transform.SetParent(healthParent.transform);
    }

    void RemoveHeart(int n)
    {
        if(healthParent.transform.childCount > 0)
        {
            if(healthParent.transform.childCount < n)
            {
                n = healthParent.transform.childCount;
            }
            for(int i = 0; i < n; i++)
            {
                Destroy(healthParent.transform.GetChild(0).gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2dPlayer.AddForce(input * speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Space) && coolDown <= 0)
        {   
            Shoot();
        }
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if(iframe > 0)
        {
            iframe -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        for(int i = 0; i < bulletSpawnPositions.Length; i++)
        {
            Instantiate(bullet, bulletSpawnPositions[i].transform.position, Quaternion.identity);
        }
        Instantiate(flash, transform.position, Quaternion.identity);

        coolDown = timeBetweenShots;
    }

    public void TakeDamage(int damage)
    {
        if(iframe <= 0)
        {
            RemoveHeart(damage);
            health = health - damage;
            if(health <= 0)
            {
                GameOver();
            }
            iframe = timeBetweenDamage;
        }
    }

    void GameOver()
    {
        FindObjectOfType<GameController>().gameOver = true;
        FindObjectOfType<GameController>().gameOverUI.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
