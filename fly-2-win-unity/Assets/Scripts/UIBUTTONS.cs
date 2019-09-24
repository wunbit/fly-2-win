using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBUTTONS : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene("Start");
    }
    public void GoToLeaderBoardButton()
    {
        SceneManager.LoadScene("Highscore");
    }
}
