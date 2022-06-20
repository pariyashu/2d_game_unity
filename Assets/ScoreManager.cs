using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highScoreText;
    public int score = 0;
    public int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore",0);
        scoreText.text =  "Score Points: " + score.ToString() ;
        highScoreText.text = "High Score: " + highScore.ToString() ;
    }
    public void Awake(){
        instance = this;
    }

    // Update is called once per frame
    public void AddPoint()
    {
        score += 10;
        scoreText.text = "Score Points: " + score.ToString();
        if (score > highScore)
        {
             PlayerPrefs.SetInt("highScore", score);
        }
    }
       
}
