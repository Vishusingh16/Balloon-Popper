using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScipt : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
