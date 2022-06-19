using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public int score = 0;
    public int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text =  " Score Points: " + score.ToString() ;
        highScoreText.text = "High Score: " + highScore.ToString() ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
