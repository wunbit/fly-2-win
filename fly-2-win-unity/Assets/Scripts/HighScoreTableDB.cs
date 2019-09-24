using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HighScoreTableDB : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private string jSonStringDB;
    public float templateHeight = 50f;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;
    private HighScores highScore;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = transform.Find("HighScoreEntryContainer/HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        StartCoroutine(GetInfofromDB());
    }

    
    private void BuildHighScoreTable()
    {
        HighScores highScores = GetHighScores();
        highScoreEntryList = highScores.highScoreEntryList;

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }


    private HighScores GetHighScores()
    {
        HighScores highScoresT = JsonUtility.FromJson<HighScores>(jSonStringDB);
        return highScoresT;
    }

    IEnumerator GetInfofromDB()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/TopTenInfo.php"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //userName.text = www.downloadHandler.text;
                jSonStringDB = www.downloadHandler.text;
                highScore = JsonUtility.FromJson<HighScores>(jSonStringDB);
            }
            BuildHighScoreTable();
        }
    }


    [System.Serializable]
    public class HighScoreEntry
    {
        public int HighScore;
        public string username;
    }
    public class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }


    public void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2 (0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        int rank = transformList.Count + 1;
        string rankString;
        Text posText = entryTransform.Find("PosText").GetComponent<Text>();
        Text scoreText = entryTransform.Find("ScoreText").GetComponent<Text>();
        Text nameText = entryTransform.Find("NameText").GetComponent<Text>();
        Image trophy = entryTransform.Find("Trophy").GetComponent<Image>();
        switch (rank)
        {
            default: rankString = rank + "th"; trophy.gameObject.SetActive(false); break;
            case 1: rankString = "1st"; trophy.color = new Color(1f, 1f, 0f, 1f); break;
            case 2: rankString = "2nd"; trophy.color = new Color(0.6f, 0.6f, 0.6f, 1f); break;
            case 3: rankString = "3rd"; trophy.color = new Color(0.7f, 0.5f, 0.2f, 1f); break;
        }

        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        if (rank == 1)
        {
            posText.color = Color.green;
            scoreText.color = Color.green;
            nameText.color = Color.green;
        }
        posText.text = rankString;
        scoreText.text = highScoreEntry.HighScore.ToString();
        nameText.text = highScoreEntry.username;
        transformList.Add(entryTransform);
    }
}


