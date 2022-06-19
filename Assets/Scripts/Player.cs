using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public float jumpVelocity;
    public Vector2 velocity;
    public float gravity;
    // game object to keep in layers 
    public LayerMask wallMask;
    public LayerMask floorMask;
    
    //states for input
    private bool walk,left_walk, right_walk,jump;
    // track player current states 
    public enum PlayerState {
        idle,
        walking,
        jumping
 
    }
    private PlayerState playerState = PlayerState.idle;
    private bool grounded = false;
    
    void Start()
    {
       Fall(); 
    }

    void Update()
        {
            CheckPlayerInput();
            UpdatePlayerPosition();
            UpdateAnimationStates();
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
            // avoide double jump when player press space bar 
            if (jump && playerState != PlayerState.jumping){
                playerState = PlayerState.jumping;
                velocity = new Vector2(velocity.x, jumpVelocity);
            }
            if (playerState == PlayerState.jumping){
                pos.y += velocity.y * Time.deltaTime;
                velocity.y -= gravity * Time.deltaTime;
            }
            if (velocity.y <= 0){
                pos = CheckFloorRays(pos);
            }
            if (velocity.y >= 0){
                pos = CheckCeilingRays(pos);
            }
            transform.localPosition = pos;
            transform.localScale = scale;
        }
        // function for Animation different based on states
        void UpdateAnimationStates(){
            if (grounded && !walk){
                GetComponent<Animator>().SetBool("isJumping", false);
                GetComponent<Animator>().SetBool("isRunning", false);
            }
            if (grounded && walk){
                GetComponent<Animator>().SetBool("isJumping", false);
                GetComponent<Animator>().SetBool("isRunning", true);
            }
            if (playerState== PlayerState.jumping){
                GetComponent<Animator>().SetBool("isJumping", true);
                GetComponent<Animator>().SetBool("isRunning", false);
                
            }
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
        Vector3 CheckWallRays(Vector3 pos, float direction){
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
            }
                return pos;
            }


    Vector3 CheckFloorRays(Vector3 pos){
        // stop player falling from ground by adding collission condition
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y-1f);
        Vector2 originRight = new Vector2(pos.x +0.5f- 0.2f, pos.y -1f);
        //get hit information
        RaycastHit2D floorLeft = Physics2D.Raycast(originLeft,Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle,Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorRight = Physics2D.Raycast(originRight,Vector2.down, velocity.y * Time.deltaTime, floorMask);
    
    if (floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null)
    {
        RaycastHit2D hitRay = floorRight;
        if (floorLeft){
            hitRay = floorLeft;
        }
        else if(floorMiddle){
            hitRay = floorMiddle;
        }
        else if (floorRight){
            hitRay = floorRight;
        }
        // player colided with enemy from top to down then kill the enemy
        if (hitRay.collider.tag == "Enemy") {
            hitRay.collider.GetComponent<EnemyControl>().Crush();
        }
        // if ground then set grounded to true
        velocity.y = 0;
        grounded = true;
        playerState = PlayerState.idle;
        pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 1;

    }
    else {
        if (playerState != PlayerState.jumping){
            Fall();
        }
    }
    return pos;
    }
// check if player hit something on top like bricks or enemies
    Vector3 CheckCeilingRays(Vector3 pos){
        // stop player falling from ground by adding collission condition
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y + 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y + 1f);
        Vector2 originRight = new Vector2(pos.x +0.5f- 0.2f, pos.y + 1f);
        //get hit information
        RaycastHit2D ceilingLeft = Physics2D.Raycast(originLeft,Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilingMiddle = Physics2D.Raycast(originMiddle,Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilingRight = Physics2D.Raycast(originRight,Vector2.up, velocity.y * Time.deltaTime, floorMask);
    if (ceilingLeft.collider != null || ceilingMiddle.collider != null || ceilingRight.collider != null)
    {
        RaycastHit2D hitRay = ceilingRight;
        if (ceilingLeft){
            hitRay = ceilingLeft;
        }
        else if(ceilingMiddle){
            hitRay = ceilingMiddle;
        }
        else if (ceilingRight){
            hitRay = ceilingRight;
        }

        velocity.y = 0;
        pos.y = hitRay.collider.bounds.center.y - hitRay.collider.bounds.size.y / 2 - 1;
    Fall();
    }
    return pos;
    }
    void Fall(){
        velocity.y = 0;
        playerState = PlayerState.jumping;
        grounded = false;
    }
     
}


