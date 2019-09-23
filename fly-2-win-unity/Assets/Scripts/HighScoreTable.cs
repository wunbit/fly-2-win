using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public float templateHeight = 50f;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;
    // Start is called before the first frame update
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = transform.Find("HighScoreEntryContainer/HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        RandomHighScoreList();
        BuildHighScoreTable();
    }

    private void BuildHighScoreTable()
    {
        HighScores highScores = GetHighScores();
        highScoreEntryList = highScores.highScoreEntryList;
        SortHighScores(highScoreEntryList);
        List<HighScoreEntry> topTen = GetTopTen(highScoreEntryList);

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in topTen)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }

    private List<HighScoreEntry> GetTopTen (List<HighScoreEntry> highScoreEntryListT)
    {
        List<HighScoreEntry> topTen = new List<HighScoreEntry>();
        if (highScoreEntryListT.Count == 10)
        {
            for (int i = 0; i < 10; i++)
            {
                HighScoreEntry tmp = highScoreEntryListT[i];
                topTen.Add(tmp);
            }
        }
        else if (highScoreEntryListT.Count < 10)
        {
            for (int i = 0; i < highScoreEntryListT.Count; i++)
            {
                HighScoreEntry tmp = highScoreEntryListT[i];
                topTen.Add(tmp);
            }
        }
        return topTen;
    }

    private void AddHighScoreEntry (int score, string name)
    {
        if (CheckForScores())
        {
            HighScoreEntry highScoreEntryT = new HighScoreEntry {score = score, name = name};
            string jSonString = PlayerPrefs.GetString("highScoreTable");
            HighScores highScoresT = JsonUtility.FromJson<HighScores>(jSonString);
            highScoresT.highScoreEntryList.Add(highScoreEntryT);
            string json = JsonUtility.ToJson(highScoresT);
            PlayerPrefs.SetString("highScoreTable", json);
            PlayerPrefs.Save();
        }
        else
        {
            HighScoreEntry highScoreEntryT = new HighScoreEntry {score = score, name = name};
            List<HighScoreEntry> highScoreEntryListT = new List<HighScoreEntry>() {highScoreEntryT};
            HighScores highScoresT = new HighScores{highScoreEntryList = highScoreEntryListT};
            string json = JsonUtility.ToJson(highScoresT);
            PlayerPrefs.SetString("highScoreTable", json);
            PlayerPrefs.Save();
        }
    }

    private void SortHighScores(List<HighScoreEntry> highScoreEntryListT)
    {
        for (int i = 0; i < highScoreEntryListT.Count; i++)
        {
            for (int j = i; j < highScoreEntryListT.Count; j++)
            {
                if (highScoreEntryListT[j].score > highScoreEntryListT[i].score)
                {
                    HighScoreEntry tmp = highScoreEntryListT[i];
                    highScoreEntryListT[i] = highScoreEntryListT[j];
                    highScoreEntryListT[j] = tmp;
                }
            }
        }
    }

    private HighScores GetHighScores()
    {
        if (!CheckForScores())
        {
            CreateNullHighScoreTable();
        }
        string jSonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScoresT = JsonUtility.FromJson<HighScores>(jSonString);
        return highScoresT;
    }

private void CreateNullHighScoreTable()
{
    HighScoreEntry highScoreEntryT = new HighScoreEntry {score = 0, name = "NUL"};
    List<HighScoreEntry> highScoreEntryListT = new List<HighScoreEntry>() {highScoreEntryT};
    HighScores highScoresT = new HighScores{highScoreEntryList = highScoreEntryListT};
    string json = JsonUtility.ToJson(highScoresT);
    PlayerPrefs.SetString("highScoreTable", json);
    PlayerPrefs.Save();
}
    private void RandomHighScoreList()
    {
        List<HighScoreEntry> highScoreEntryListT = new List<HighScoreEntry>()
        {
            new HighScoreEntry{ score = 521854, name = "AAA"},
            new HighScoreEntry{ score = 851123, name = "CAT"},
            new HighScoreEntry{ score = 215360, name = "JON"},
            new HighScoreEntry{ score = 450263, name = "JOE"},
            new HighScoreEntry{ score = 69523, name = "MIK"},
            new HighScoreEntry{ score = 423684, name = "DAV"},
            new HighScoreEntry{ score = 23081, name = "MAX"},
            new HighScoreEntry{ score = 85623, name = "KIP"},
            new HighScoreEntry{ score = 52102, name = "POE"},
            new HighScoreEntry{ score = 562114, name = "FIL"},
        };
        HighScores highScoresT = new HighScores{highScoreEntryList = highScoreEntryListT};
        string json = JsonUtility.ToJson(highScoresT);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    private bool CheckForScores()
    {
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [System.Serializable]
    public class HighScoreEntry
    {
        public int score;
        public string name;
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
        scoreText.text = highScoreEntry.score.ToString();
        nameText.text = highScoreEntry.name;
        transformList.Add(entryTransform);
    }
}


