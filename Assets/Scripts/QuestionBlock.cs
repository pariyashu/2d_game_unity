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
    IEnumerator Bounce()
    {
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
}
