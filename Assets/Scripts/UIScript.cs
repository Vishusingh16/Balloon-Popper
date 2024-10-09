using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIScript : MonoBehaviour
{
     public void StartGame()
    {
       Debug.Log("Start button clicked");
        SceneManager.LoadScene("GameScene");

    }

   
    public void RestartGame()
    {
          SceneManager.LoadScene("GameScene");
    }

    
    public void QuitGame()
    {
        
        Application.Quit();

    
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
}
public void LoadMainMenu()
    {
       
        SceneManager.LoadScene("StartMenu"); 
    }
}