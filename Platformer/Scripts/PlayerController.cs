using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D playerRigidbody;
    float inputX;

    public LayerMask wallLayer;
    public float rayLength;
    bool canJump;
    public float jumpHeight;

    bool hurt;
    public float maxHealth;
    [SerializeField]
    float health;
    public float timeBetweenHurt;
    float iframe;

    Animator anim;
    SpriteRenderer rend;

    int coins = 0;

    public Image healthImage;
    public Text coinsText;

    public GameObject gameoverUI;
    bool gameover;

    public GameObject gamewinUI;
    bool gamewin;

    private void Awake() 
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        canJump = true;
        health = maxHealth;
        hurt = false;
        iframe = timeBetweenHurt;
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        gameover = false;
        gamewin = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if(inputX != 0)
        {
            playerRigidbody.AddForce(Vector2.right * inputX * speed * Time.deltaTime);

        }
        rend.flipX = (inputX < 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, wallLayer);

        if(hit.collider != null)
        {
            canJump = true;
        }
        if(canJump && Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector2.up * jumpHeight);
            canJump = false;
        }

        Debug.DrawRay(transform.position, Vector2.down * rayLength);

        if(iframe > 0) iframe =- Time.deltaTime;

        // test damage function
        if(!hurt && Input.GetKeyDown(KeyCode.LeftControl))
            Damage(2);

        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, health/maxHealth, Time.deltaTime * 10f);
        coinsText.text = "X " + coins.ToString();

        anim.SetBool("moving", inputX !=  0);
        anim.SetBool("canJump", canJump);
        anim.SetBool("hurt", hurt);

        if(gameover && Input.anyKeyDown)
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1f;
        }
        if (gamewin && Input.anyKeyDown)
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1f;
        }
    }

    public void Damage(float amt)
    {
        if(iframe < 0)
        {
            health -= amt;
            hurt = true;
            Invoke("ResetHurt", 0.2f);
            if(health <= 0)
            {
                GameOver();
            }
            iframe = timeBetweenHurt;
        }
    }

    void GameOver()
    {
        gameover = true;
        gameoverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void GameWin()
    {
        gamewin = true;
        gamewinUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void ResetHurt()
    {
        hurt = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            coins++;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("BigCoin"))
        {
            coins += 10;
            collision.gameObject.SetActive(false);
            GameWin();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && playerRigidbody.velocity.y < 0)
        {
            float boundsY = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            if(transform.position.y > collision.gameObject.transform.position.y + boundsY)
            {
                playerRigidbody.AddForceAtPosition(-playerRigidbody.velocity.normalized * jumpHeight/2, playerRigidbody.position);
                collision.gameObject.GetComponent<EnemyController>().Damage(10f);
            }
        }
    }
}
