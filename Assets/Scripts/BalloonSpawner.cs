using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using System.Collections.Generic;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject[] balloonPrefabs;
    public float spawnRate;
    public Transform[] spawnPoints;

    private float timer = 0;
    private ObjectPool<GameObject> balloonPool;
    public Slider balloonCountSlider;
    private List<GameObject> activeBalloons = new List<GameObject>();

    public static BalloonSpawner Instance { get; private set; }

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

    void Start()
    {
        balloonPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(balloonPrefabs[0]),
            actionOnGet: (balloon) =>
            {
                balloon.SetActive(true);
                activeBalloons.Add(balloon);
            },
            actionOnRelease: (balloon) =>
            {
                balloon.SetActive(false);
                activeBalloons.Remove(balloon);
            },
            actionOnDestroy: (balloon) => Destroy(balloon),
            defaultCapacity: 10,
            maxSize: 20
        );

        balloonCountSlider.value = Mathf.InverseLerp(0.1f, 3f, 1f);
        balloonCountSlider.onValueChanged.AddListener(OnBalloonCountChanged);
        OnBalloonCountChanged(balloonCountSlider.value);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnBalloon();
            timer = 0;
        }
    }

    void SpawnBalloon()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points are not assigned in the BalloonSpawner.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);

        if (spawnPoints[randomIndex] == null)
        {
            Debug.LogError("One of the spawn points is missing.");
            return;
        }

        GameObject balloon = balloonPool.Get();
        
        if (balloon == null)
        {
            Debug.LogError("Failed to get a balloon from the pool.");
            return;
        }

        balloon.transform.position = spawnPoints[randomIndex].position;

        int balloonType = Random.Range(0, balloonPrefabs.Length);
        balloon.GetComponent<SpriteRenderer>().sprite = balloonPrefabs[balloonType].GetComponent<SpriteRenderer>().sprite;
        balloon.transform.localScale = balloonPrefabs[balloonType].transform.localScale;

        SpriteRenderer sr = balloon.GetComponent<SpriteRenderer>();
        sr.color = new Color(Random.value, Random.value, Random.value);

        BalloonMovement balloonMovement = balloon.GetComponent<BalloonMovement>();
        balloonMovement.speed = Random.Range(1f, 3f);
    }


    public void ReleaseBalloon(GameObject balloon)
    {
        balloonPool.Release(balloon);
    }

    public void DecreaseSpawnRate(float amount)
    {
        spawnRate = Mathf.Max(spawnRate - amount, DifficultyManager.Instance.minSpawnRate);
    }

    // public void IncreaseBalloonSpeed(float amount)
    // {
    //     foreach (GameObject balloon in activeBalloons)
    //     {
    //         if (balloon.activeSelf)
    //         {
    //             BalloonMovement balloonMovement = balloon.GetComponent<BalloonMovement>();
    //             balloonMovement.speed = Mathf.Min(balloonMovement.speed + amount, DifficultyManager.Instance.maxBalloonSpeed);
    //         }
    //     }
    // }


    public void IncreaseBalloonSpeed(float amount)
{
    foreach (GameObject balloon in activeBalloons)
    {
        if (balloon != null && balloon.activeSelf) 
        {
            BalloonMovement balloonMovement = balloon.GetComponent<BalloonMovement>();
            if (balloonMovement != null) 
            {
                balloonMovement.speed = Mathf.Min(balloonMovement.speed + amount, DifficultyManager.Instance.maxBalloonSpeed);
            }
        }
    }
}


    void OnBalloonCountChanged(float value)
    {
        spawnRate = Mathf.Lerp(0.2f, 3f, 1f - value);
    }

    public void ResetSpawner()
    {
        foreach (GameObject balloon in activeBalloons)
        {
            if (balloon.activeSelf)
            {
                ReleaseBalloon(balloon);
            }
        }
        activeBalloons.Clear();
        timer = 0;
        spawnRate = 1f;
    }
}
