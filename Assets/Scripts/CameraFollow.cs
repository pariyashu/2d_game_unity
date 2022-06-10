using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
public Transform target;
public Transform leftBounds;
public Transform rightBounds;
private float camWidth, camHeight, levelMinX, levelMaxX;
{
    // set up camera bounds 
    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;
        float leftBoundWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        levelMinX = leftBounds.position.x + leftBoundWidth + camWidth / 2;
        levelMaxX = rightBounds.position.x - rightBoundWidth - camWidth / 2;
    }

    // follow player
    void Update()
    { 
        if (target){
            // Set camera to follow player
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));
            float targetY = target.position.y;
            Vector3 v = new Vector3(targetX, targetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, v, 0.1f);
        }
    }
}
