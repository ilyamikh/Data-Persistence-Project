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
    }

    public void SaveScores()
    {
        Score data = new Score(highScoreOwner, highScore);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/score.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Score data = JsonUtility.FromJson<Score>(json);

            highScoreOwner = data.ownerName;
            highScore = data.score;

            scores.Add(new Score(highScoreOwner, highScore));
        }
    }
}
