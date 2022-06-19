using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 4f;
    // variable to control state of the question block to make bounce only once 
    private bool coinBounce = true;
    // to bounce back to same position store original position 
    private Vector2 originalPosition;
    // acces the current sprite to replace it 
    public Sprite emptyBlockSprite;

    public float coinBounceSpeed = 8f;
    public float coinBounceHeight = 3f;
    public float coinFallDistacne = 2f;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }
    public void QuestionBlockBounce()
    {
        if (coinBounce)
        {
            coinBounce = false;
            
            StartCoroutine(Bounce());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void PresentCoin(){
        // instiance the coin prefab
        GameObject spinningCoin = (GameObject)Instantiate(Resources.Load("Prefabs/Spinning_Coin", typeof(GameObject)));
        spinningCoin.transform.SetParent(this.transform.parent);
        // instiantiate coin one unit higher than the block
        spinningCoin.transform.localPosition = new Vector2(originalPosition.x, originalPosition.y + 1);
        StartCoroutine(MoveCoin(spinningCoin));
    }

    void ChangeSprite(){
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = emptyBlockSprite;
    }

    IEnumerator Bounce()
    {
        // once hit change the sprite to empty block
        ChangeSprite();
        PresentCoin();
        // to make the question block bounce up
        while (true)
        {
            transform.localPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);

            if (transform.localPosition.y >= originalPosition.y + bounceHeight)
            {
               break;
            }
            yield return null;


        }
        // make sure the question block is at the original position after bounce to down
        while (true)
        {
            transform.localPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime);

            if (transform.localPosition.y <= originalPosition.y)
            {
                transform.localPosition = originalPosition;
                break;
            }
            yield return null;
        }
    }

    IEnumerator MoveCoin(GameObject coin){
        // to make the coin move up
        while(true){
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + coinBounceSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y >= originalPosition.y + coinBounceHeight+ 1)
            {
                break;
            }
            yield return null;
        }
        // to make the coin move down
        while(true){
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y - coinBounceSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y <= originalPosition.y + coinFallDistacne + 1)
            {
                Destroy(coin.gameObject);
                break;
            }
            yield return null;
        }

    }
}
