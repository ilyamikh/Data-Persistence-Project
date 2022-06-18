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

        foreach(string item in scoreManager.scores)
        {
            string scoreString = item + System.Environment.NewLine;
            scoreList += scoreString;
        }

        return scoreList;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
