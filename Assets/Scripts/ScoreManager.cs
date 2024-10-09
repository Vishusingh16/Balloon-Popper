using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score { get; private set; }



    public TMP_Text scoreText;     
    public TMP_Text highScoreText; 
    public TMP_Text timerText;  

    public TMP_Text popup;

    public float elapsedTime;    

    
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

    private void Start()
    {
        popup.text = "";
        UpdateScoreText();
        elapsedTime = 0f;  
    }

    private void Update()
    {
        UpdateTimer();
    }

   

    public void AddScore(int points)
    {

        score += points;   
        if (score >= 300 && score < 400) 
        {
            popup.text ="Power Up 1 Activated!";
            score +=20;     

        }
        else if (score >= 600 && score < 700) 
        {
            score+=30;
            popup.text ="Power Up 2 Activated!";
        }   
        else{
            popup.text ="";
        }     
        UpdateScoreText();
        Debug.Log(score);        
        CheckAndUpdateHighScore(); 
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();  
    }


    private void UpdateScoreText()
    {
       
        if (scoreText != null)
        {
            scoreText.text = "Score: " + (score / 10).ToString();  
        }

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + (highScore / 10).ToString(); 
        }
    }

    public void CheckAndUpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score); 
            PlayerPrefs.Save(); 

            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + (score / 10).ToString();
            }
        }
    }

    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;  
        int seconds = (int)(elapsedTime % 60);  
        int milliseconds = (int)((elapsedTime * 1000) % 1000);  

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:000}", seconds, milliseconds);
        }

       if (elapsedTime >= 80f)
    {
        SceneManager.LoadScene("EndGame");
    }
    }
}
