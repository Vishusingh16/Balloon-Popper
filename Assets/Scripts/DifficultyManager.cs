using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    public float minSpawnRate = 0.5f;  
    public float maxBalloonSpeed = 5f;  
    public float difficultyIncreaseInterval = 30f; 

    private float difficultyTimer;
    private int difficultyLevel = 1;

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
        difficultyTimer = 0;
    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= difficultyIncreaseInterval)
        {
            IncreaseDifficulty();
            difficultyTimer = 0;  
        }
    }

    private void IncreaseDifficulty()
    {
        difficultyLevel++;
        BalloonSpawner.Instance.DecreaseSpawnRate(0.1f);
        BalloonSpawner.Instance.IncreaseBalloonSpeed(0.2f);

        Debug.Log($"Difficulty increased to level {difficultyLevel}. Spawn rate and balloon speed adjusted.");
    }
}
