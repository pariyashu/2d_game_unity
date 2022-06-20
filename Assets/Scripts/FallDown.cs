using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDown : MonoBehaviour
{
    
    void OnTriggerEnter2D (Collider2D other)
    {
        Vector3 pos = transform.localPosition;
        if (other.tag == "Player")
        {
            // Log.debug("Player hit");
            pos.y = 2.5f;
            pos.x = 0.0f;
            pos.z = -1.7f;
            other.transform.localPosition = pos;
            ScoreManager.instance.SubtractLife();

        }
    }
}
