using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 velocity;
    //states for input
    private bool walk,left_walk, right_walk,jump;
    
    void Start()
    {
        
    }

    void Update()
        {
            CheckPlayerInput();
            UpdatePlayerPosition();
        }
        void UpdatePlayerPosition () {
            Vector3 pos = transform.localPosition;
            // get the face direction
            Vector3 scale = transform.localScale;
            if (walk){
                if (left_walk){
                    // change the face direction to left and walk on left side
                    pos.x -= velocity.x * Time.deltaTime;
                    scale.x = -1;
                }
                if (right_walk){
                    // change the face direction to right and walk on right side
                    pos.x += velocity.x * Time.deltaTime;
                    scale.x = 1;
                }
            }
            transform.localPosition = pos;
            transform.localScale = scale;
        }
        void CheckPlayerInput()
        {
            bool input_left = Input.GetKey(KeyCode.LeftArrow);
            bool input_right = Input.GetKey(KeyCode.RightArrow);
            bool input_space = Input.GetKey(KeyCode.Space);
            walk = input_left || input_right;
            left_walk = input_left && !input_right;
            right_walk = input_right && !input_left;
            jump = input_space;
        }
}

