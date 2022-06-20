using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDown : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            ScoreManager.instance.SubtractLife();
           // Log.debug("Player hit");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
