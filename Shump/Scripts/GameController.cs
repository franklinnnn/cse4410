using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] enemies;
    public float timeBetweenSpawnLow = 0.5f;
    public float timeBetweenSpawnHigh = 3f;

    float spawnCoolDown;
    Vector2 bounds;
    Vector3 spawnPosition;

    public Text scoreText;
    int scores;
    public int clearScore;

    public bool gameOver;
    public GameObject gameOverUI;
    public GameObject nextLevelUI;

    // Start is called before the first frame update
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        spawnCoolDown = Random.Range(timeBetweenSpawnLow, timeBetweenSpawnHigh);
        scores = 0;
        scoreText.text = "SCORE: " + scores.ToString();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCoolDown > 0)
        {
            spawnCoolDown -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
        }
        scoreText.text = "SCORE: " + scores.ToString();
        if(gameOver && Input.anyKeyDown)
        {
            Restart();
        }

        if(scores >= clearScore)
        {
            nextLevelUI.SetActive(true);
            Invoke("NextLevel", 2f);
        }
    }

    void SpawnEnemy()
    {
        spawnPosition = new Vector3(Random.Range(-bounds.x + 1f, bounds.x - 1f), (bounds.y + Random.Range(0.25f, 3f)), 0);
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.Euler(0, 0, 180));
        spawnCoolDown = Random.Range(timeBetweenSpawnLow, timeBetweenSpawnHigh);
    }

    public void AddScore(int amount)
    {
        scores += amount;
        
    }

    void Restart()
    {
        SceneManager.LoadScene("Level 01");
        Time.timeScale = 1f;
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Level 02");
        Time.timeScale = 2f;
    }
}
