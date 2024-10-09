using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
        }
    }

  
    public void ResetScore()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();  
        }
    }

  
    public void CheckHighScore()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.CheckAndUpdateHighScore(); 
        }
    }
}
