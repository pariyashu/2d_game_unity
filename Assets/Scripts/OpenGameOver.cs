using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGameOver : MonoBehaviour
{
public void RestartGame(){
    SceneManager.LoadScene("gameover");

}
public void QuitGame(){
    Debug.Log("Quit");
    Application.Quit();
}
}
