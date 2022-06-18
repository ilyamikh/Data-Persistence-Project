using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private string playerName;
    private string highScoreOwner;
    private int highScore;
    private int sessionScore;

    public List<Score> scores = new List<Score>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        playerName = "Player Name";
        LoadScores();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetName(string inName)
    {
        playerName = inName;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetSessionScore(int score)
    {
        sessionScore = score;
        AddScore();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (sessionScore > highScore)
        {
            highScore = sessionScore;
            highScoreOwner = playerName;
        }
    }

    private void AddScore()
    {
        if(scores.Count <= 10)
            scores.Add(new Score(playerName, sessionScore));
    }
    public string GetHighScoreString()
    {
        return highScoreOwner + ": " + highScore;
    }
    
    [System.Serializable]
    public class Score
    {
        public string ownerName;
        public int score;

        public Score(string inName, int pts)
        {
            ownerName = inName;
            score = pts;
        }

        public string GetScoreString()
        {
            return ownerName + ": " + score;
        }
    }

    [System.Serializable]
    public class ScoreArray
    {
        public string[] players;
        public int[] scores;

        public ScoreArray(List<Score> inList)
        {
            players = new string[inList.Count];
            scores = new int[inList.Count];

            for (int i = 0; i < inList.Count; i++){
                players[i] = inList[i].ownerName;
                scores[i] = inList[i].score;
            }
        }

        public void ShowScores()
        {
            for(int i = 0; i < players.Length; i++)
            {
                Debug.Log(players[i] + ": " + scores[i] + System.Environment.NewLine);
            }
        }
    }

    [System.Serializable]
    public class ScoreList
    {
        public List<Score> allScores = new List<Score>();

        public ScoreList(List<Score> inList)
        {
            allScores = inList;
        }

        public void ShowScores()
        {
            foreach(Score item in allScores)
            {
                Debug.Log(item.GetScoreString() + System.Environment.NewLine);
            }
        }
    }
    public void SaveHighScore()
    {
        Score data = new Score(highScoreOwner, highScore);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void SaveScoreList()
    {
        ScoreList saveScores = new ScoreList(scores);
        string json = JsonUtility.ToJson(saveScores);
        File.WriteAllText(Application.persistentDataPath + "/allscores.json", json);
    }

    public void LoadScores()
    {
        string highScorePath = Application.persistentDataPath + "/highscore.json";
        string allScorePath = Application.persistentDataPath + "/allscores.json";

        if (File.Exists(highScorePath))
        {
            string json = File.ReadAllText(highScorePath);
            Score data = JsonUtility.FromJson<Score>(json);

            highScoreOwner = data.ownerName;
            highScore = data.score;

        }

        if (File.Exists(allScorePath))
        {
            string json = File.ReadAllText(allScorePath);
            ScoreList data = JsonUtility.FromJson <ScoreList>(json);
            scores = data.allScores;
        }

    }
}
