﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
   public static GameControl instance;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;

    
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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown (KeyCode.Space))    // I dont need to put gameOver == true, since this is and it checks if the variable is true since the value in true is there (I think)
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        }
    }

        public void BirdDied() 
    {
        gameOverText.SetActive(true);
        gameOver = true; 
    }
}