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
    public string GetHighScoreString()
    {
        return highScoreOwner + ": " + highScore;
    }

    [System.Serializable]
    class Scores
    {
        public string ownerName;
        public int score;
    }

    public void SaveScores()
    {
        Scores data = new Scores();
        data.ownerName = highScoreOwner;
        data.score = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/score.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Scores data = JsonUtility.FromJson<Scores>(json);

            highScoreOwner = data.ownerName;
            highScore = data.score;
        }
    }
}
