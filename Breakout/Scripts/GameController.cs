using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    public int lives = 3;
    [SerializeField]
    private Text livesText;

    int numOfBricks;
    [SerializeField]
    private Text bricksText;

    public GameObject gameOverUI;
    bool gameOver;
    
    public GameObject levelClearedUI;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "LIVES " + lives.ToString();
        numOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        bricksText.text = numOfBricks.ToString() + " BRICKS";
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.anyKeyDown)
        {
            Restart();
        }
    }

    public void LoseLife() // decrement number of lives
    {
        lives--;
        livesText.text = "LIVES " + lives.ToString();
        if(lives == 0)
        {
            GameOver();
        }
    }

    public void HitBrick() // decrement number of bricks
    {
        numOfBricks--;
        bricksText.text = numOfBricks.ToString() + " BRICKS";
        if(numOfBricks <=0)
        {
            levelClearedUI.SetActive(true);
            Invoke("NextLevel", 2f);
        }
    }

    void GameOver()
    {
        gameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Restart()
    {
        SceneManager.LoadScene("Level 01");
        Time.timeScale = 1f;
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Level 02");
    }
}
