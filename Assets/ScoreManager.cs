using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highScoreText;
    public Text lifeLeftText;
    public int score = 0;
    public int highScore = 0;
    public int lifeLeft = 3;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore",0);
        lifeLeftText.text = "Life Left: " + lifeLeft.ToString();
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
    public void SubtractLife()
    {
        Debug.Log("substrateted the life ");
        lifeLeft--;
        lifeLeftText.text = "Life Left: " + lifeLeft.ToString();
        if (lifeLeft == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
       
}
