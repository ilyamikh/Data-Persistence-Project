using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
