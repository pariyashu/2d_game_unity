using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    
    public Vector2 velocity;
    // game object to keep in layers 
    public LayerMask wallMask;
    
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
                // use raycast to check if there is a wall in front of the player
                pos = CheckWallRays(pos, scale.x);
                
            }
            transform.localPosition = pos;
            transform.localScale = scale;
        }
        void CheckPlayerInput()
        {
            // input key for walk, left_walk, right_walk, jump
            bool input_left = Input.GetKey(KeyCode.LeftArrow);
            bool input_right = Input.GetKey(KeyCode.RightArrow);
            bool input_space = Input.GetKey(KeyCode.Space);
            walk = input_left || input_right;
            left_walk = input_left && !input_right;
            right_walk = input_right && !input_left;
            jump = input_space;
        }
        Vector3 CheckWallRays(Vector3 pos, float direction)
        {
            // create rays to check if there is collission
            Vector2 originTop = new Vector2(pos.x + direction *.4f, pos.y + 1f - 0.2f);
            Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y);
            Vector2 originBottom = new Vector2(pos.x + direction *.4f, pos.y  -1f + 0.2f);
            //get hit information
            RaycastHit2D wallTop = Physics2D.Raycast(originTop,new Vector2(direction,0), velocity.x * Time.deltaTime, wallMask);
            RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle,new Vector2(direction,0), velocity.x * Time.deltaTime, wallMask);
            RaycastHit2D wallBottom = Physics2D.Raycast(originBottom,new Vector2(direction,0), velocity.x * Time.deltaTime, wallMask);
            if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null)
            {
                pos.x -= velocity.x * Time.deltaTime * direction;
                // if there is no collission, the player will keep moving in the same direction
                
                return pos;

            }
            else
            {
            // if there is collission, stop
            return pos;
            }
}
}

