using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    //Public objects for Editor
    public static GameControl instance;
    public Text scoreText;
    public Text highScoreText;
    public Text livesText;
    public ColumnPool columnPoolScript;
    public GameObject gameOverText;
    public Text gameOverTxt;
    public Text restartText;
    //Private Vars
    private int highScore;
    private float spawnRate;
    private bool freePlay = false;
    private string currentUser;
    public int lives = 0;
    //Public Vars
    public float scrollSpeed = -4f;
    [HideInInspector]
    public bool gameOver = false;
    public int round = 1;
    public int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText");
        columnPoolScript = GameObject.Find("ColumnManager").GetComponent<ColumnPool>();
        highScore = StaticVars.highScore;
        highScoreText.text = "High Score:" + ReadScore().ToString();
        freePlay = StaticVars.freePlay;
        currentUser = StaticVars.currentUser;
        //lives = StaticVars.lives;
        Debug.Log(freePlay);
        gameOverText.SetActive(false);
        if (!freePlay)
        {
            lives = CheckLives();
            livesText.text = "Lives: " + lives.ToString();
            if (lives == 0)
            {
                gameOver = true;
                SceneManager.LoadScene("ResetLives");
            }
        }
        if (freePlay)
        {
            //lives = 99;
            livesText.text = "Lives: " + "∞";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lives > 0 || freePlay)
        {
            if (gameOver && Input.GetKeyDown (KeyCode.Space) || gameOver && Input.GetMouseButtonUp (0))
        {
            gameOver = false;
            SceneManager.LoadScene("MainGame");
        }
        }
        else if (lives <= 0)
        {
            if (gameOver && Input.GetKeyDown (KeyCode.Space) || gameOver && Input.GetMouseButtonUp (0))
            {
                gameOver = false;
                SceneManager.LoadScene("ResetLives");
            }
        }
    }
        public void BirdScored()
        {
            if (gameOver)
            {
                return;
            }
            score++;
            scoreText.text = "Score: " + score.ToString();
            if (score > round * 10)
            {
                //makes the game harder every 5 scores
                round++;
                columnPoolScript.spawnRate -= 0.2f;
                scrollSpeed -= 0.2f;
            }
        }
        public void BirdDied()
    {
        if (!freePlay)
        {
            SaveScore();
            lives--;
            UpdateLives();
            lives = CheckLives();
            livesText.text = "Lives: " + lives.ToString();
        }
        gameOver = true;
        if (lives > 0 || freePlay)
        {
            gameOverTxt.text = "Continue";
            restartText.text = "Flap to Restart";
        }
        else if (lives <= 0)
        {
            gameOverTxt.text = "GAME OVER";
            restartText.text = "Flap to Go to Main Menu";
        }
        gameOverText.SetActive(true);
    }

    public int CheckLives()
    {
        if (PlayerPrefs.HasKey("Lives"))
        {
            int lives = PlayerPrefs.GetInt("Lives");
            return lives;
        }
        else
        {
            int lives = 3;
            PlayerPrefs.SetInt("Lives", lives);
            PlayerPrefs.Save();
            return lives;
        }
    }

    public void UpdateLives()
    {
        if (lives >= 0)
        {
            PlayerPrefs.SetInt("Lives", lives);
            PlayerPrefs.Save();
        }
    }

    //Local HighScore  system

    private bool CheckForScores()
    {
        if (PlayerPrefs.HasKey("PersonalHighScore"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SaveScore()
    {
        int previousScore = ReadScore();
        if (score > previousScore)
        {
            PlayerPrefs.SetInt("PersonalHighScore", score);
            PlayerPrefs.Save();
        }
    }

    private int ReadScore()
    {
        if (CheckForScores())
        {
            int bestScore = PlayerPrefs.GetInt("PersonalHighScore");
            return bestScore;
        }
        else
        {
            int bestScore = 0;
            return bestScore;
        }

    }


}
