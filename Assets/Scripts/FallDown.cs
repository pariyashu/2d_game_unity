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
            //Debug.Log("You lose!");
            SceneManager.LoadScene("GameOver");
        }
    }
}
