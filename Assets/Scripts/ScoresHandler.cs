using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoresHandler : MonoBehaviour
{
    public TMP_Text scoreText;
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        scoreText.text = CreateScoreList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string CreateScoreList()
    {   
        
        string scoreList = "";

        if (scoreManager.scores.Count < 1)
            return scoreList;
        else
        {
            scoreManager.scores.Sort();

            for(int i = scoreManager.scores.Count - 1; i >= 0; i--)
            {
                string scoreString = scoreManager.scores[i].GetScoreString() + System.Environment.NewLine;
                scoreList += scoreString;
            }
        }

        return scoreList;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
