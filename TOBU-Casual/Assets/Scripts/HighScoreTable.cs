using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] Transform scoreContainer;
    [SerializeField] Transform scoreEntry;
    [SerializeField] float entryHeight = 35f;
    [SerializeField] int scoresToDisplay = 10;
    private List<HighscoreEntry> scoreEntryList;
    private List<Transform> scoreEntryTransformList;

    Transform entryTransform;
    float anchorPointY = 0;

    private void Awake()
    {
        scoreEntry.gameObject.SetActive(false);
        //PlayerPrefs.DeleteAll();

        if (!PlayerPrefs.HasKey("highscoreTable"))
        {
            scoreEntryList = new List<HighscoreEntry>() { new HighscoreEntry { score = 0} };
            string json = JsonUtility.ToJson(scoreEntryList);
            PlayerPrefs.SetString("highscoreTable", json);
        }

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        AddEntry(GameManager.Instance.Score);

        jsonString = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonString);

        scoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, scoreContainer, scoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(scoreEntry, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -entryHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = "";
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }


        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;
        float score = highscoreEntry.score;
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

        //entryTransform.Find("DarkerRed").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1)
        {
            entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().color = Color.green;
        }

        transformList.Add(entryTransform);

        Debug.Log("InstantiationMade");
    }

    private void AddEntry(float score)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score};

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        //Sort highscore entries
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry swapper = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = swapper;
                }
            }
        }
        //Remove all scores above the maximum display count
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            if (i > scoresToDisplay - 1)
            {
                Debug.Log(i);
                highscores.highscoreEntryList.RemoveAt(i - 1);
            }
        }

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void MainMenu()
    {
        //Score.score = 0;
        //SceneManager.LoadScene("Main Menu");
    }

    public void PlayAgain()
    {
        //Score.score = 0;
        //SceneManager.LoadScene("_WinterLand");
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public float score;
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
}

