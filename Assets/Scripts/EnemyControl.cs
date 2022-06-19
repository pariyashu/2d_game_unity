using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // to pull enemy down d´when fall of the edge
    public float gravity;
    // make first enemy walk on left side at the start of the game
    public bool isWalkingLeft = true;
    // how fast the enemy walks
    public Vector2 velocity;
    // at beginning the enemy is not grounded
    public bool grounded = false;
    // to check if enemy hit ground mask
    public LayerMask floorMask;
    // to check if enemy hit wall mask
    public LayerMask wallMask;

    // starting states of enemy 
    private enum EnemyState{
        walking,
        falling,
        dead
    }
    // state enemy state at beginning to falling
    private EnemyState state = EnemyState.falling;
    // Start is called before the first frame update
    void Start()
    {
        // to set the walking states only if the enemy is in camera view area
        enabled = false;
        Fall();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyPosition();
    }
    // to update the enemy position if enemy is not dead

    void UpdateEnemyPosition(){
        if (state != EnemyState.dead)
        {
            Vector3 pos = transform.localPosition;
            Vector3 scale = transform.localScale;
            if ( state == EnemyState.falling){
                pos.y += velocity.y * Time.deltaTime;
                velocity.y -= gravity * Time.deltaTime;
            }
            if (state == EnemyState.walking){
                if (isWalkingLeft){
                    pos.x -= velocity.x * Time.deltaTime;
                    scale.x =-1;
                }
                else{
                    pos.x += velocity.x * Time.deltaTime;
                    scale.x = 1;
                }
            }
            if (velocity.y < 0){
               pos = CheckGround(pos);
               
            }
            CheckWalls(pos,scale.x);
            transform.localPosition = pos;
            transform.localScale = scale;
        }
    }
    Vector3 CheckGround (Vector3 pos){
        Vector2 originLeft = new Vector2 (pos.x -0.5f  + 0.2f, pos.y -0.5f);
        Vector2 originMiddle = new Vector2 (pos.x , pos.y -0.5f);
        Vector2 originRight = new Vector2 (pos.x +0.5f  - 0.2f, pos.y -0.5f);
        RaycastHit2D groundLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime,floorMask);
        RaycastHit2D groundMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime,floorMask);
        RaycastHit2D groundRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime,floorMask);
        if (groundLeft.collider !=  null || groundMiddle.collider !=  null || groundRight.collider !=  null){
           RaycastHit2D hitRay = groundLeft;
           if (groundLeft){
                hitRay = groundLeft;
              }
              else if (groundMiddle){
                hitRay = groundMiddle;
              }
              else if (groundRight){
                hitRay = groundRight;
           }
           // chek if collided with player kill the player 
              if (hitRay.collider.tag == "Player"){
                Application.LoadLevel("GameOver");
              }
           // enemy when colliding with the ground is grounded and start walking    
           pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 +0.5f;
           grounded = true;
           velocity.y = 0;
           state = EnemyState.walking;

        }
        else{
            // if not in falling state call falling state
            if (state != EnemyState.falling){
                Fall();
            }
        }
        return pos;
    }

    // check walls if enemy collided with wall start moving in oppisite direction 
    void CheckWalls(Vector3 pos,float direction){
        Vector2 originTop = new Vector2 (pos.x +direction * 0.4f, pos.y +0.5f - 0.2f);	// 0.2f is the offset to the raycast
        Vector2 originMiddle = new Vector2 (pos.x +direction * 0.4f, pos.y);
        Vector2 originBottom = new Vector2 (pos.x +direction * 0.4f, pos.y -0.5f + 0.2f);
        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2 (direction,0), velocity.x * Time.deltaTime,wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2 (direction,0), velocity.x * Time.deltaTime,wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2 (direction,0), velocity.x * Time.deltaTime,wallMask);
        if (wallTop.collider !=  null || wallMiddle.collider !=  null || wallBottom.collider !=  null){
            RaycastHit2D hitRay = wallTop;
            if (wallTop){
                hitRay = wallTop;
            }
            else if (wallMiddle){
                hitRay = wallMiddle;
            }
            else if (wallBottom){
                hitRay = wallBottom;
            }
            // chek if collided with player kill the player 
            if (hitRay.collider.tag == "Player"){
            Application.LoadLevel("GameOver");
            }
            isWalkingLeft = !isWalkingLeft; // change direction

    }
    }
    // check if the enemy is in camera view area
    // enable script when on view area
    void OnBecameVisible()
    {
        // enable teh script only after the enemy is in camera view area
        enabled = true;
    }
    void Fall()
    {
        // make the enemy fall
        // set y velocity to 0
        velocity.y = 0;
        //change state to falling
        state = EnemyState.falling;
        grounded = false;
}
}
