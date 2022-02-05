using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    int leftPlayerScore, rightPlayerScore;
    public int maxScore = 3;
    
    public Text scoreText;

    public GameObject gameOverUI;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Score(bool leftPlayerGetScore)
    {   
        if(leftPlayerGetScore)
        {
            leftPlayerScore++;
        }
        else
        {
            rightPlayerScore++;
        }

        if(leftPlayerScore >= maxScore)
        {
            scoreText.text = "Left Player wins!";
            GameOver();
        }
        else if(rightPlayerScore >= maxScore)
        {
            scoreText.text = "Right Player wins!";
            GameOver();
        }
        else
        {
            scoreText.text = leftPlayerScore + " : " + rightPlayerScore;
        }

    }

    void GameOver()
    {
        gameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            if(Input.anyKeyDown)
            {
                Restart();
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
}
