using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;

    // speed of the camera
    public float smoothDampTime = 0.15f;
    private Vector3 smoothDampvelocity = Vector3.zero;
    private float camWidth, camHeight, levelMinX, levelMaxX;
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
            // set target so that camera stays in bounds
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));
            // method that moves the camera to the target position        
            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampvelocity.x, smoothDampTime);
            
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
