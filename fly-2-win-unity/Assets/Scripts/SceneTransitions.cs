using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator transitionAnim;
    private bool freePlay;
    // Start is called before the first frame update
    void Awake()
    {
        freePlay = StaticVars.freePlay;
    }
    
    void Start()
    {
        transitionAnim = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    public void StartGame(string sceneName)
    {
        StaticVars.freePlay = false;
        //PlayerPrefs.SetInt("Lives", 3);
        //PlayerPrefs.Save();
        StartCoroutine(Transition(sceneName));
    }

    public void FreePlayStart(string sceneName)
    {
        StaticVars.freePlay = true;
        StartCoroutine(Transition(sceneName));
    }

    /* public void Continue(string sceneName)
    {
        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.Save();
        StartCoroutine(Transition(sceneName));
    } */

    public void RestockLives(string sceneName)
    {
        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.Save();
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
