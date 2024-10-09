using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    public Camera cam; 
    public BalloonSpawner balloonSpawner; 
    public AudioClip popSound;
    public ParticleSystem popEffect; 

    private AudioSource audioSource; 

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (cam == null)
        {
            cam = Camera.main; 
            if (cam == null)
            {
                Debug.LogError("No camera found in the scene.");
            }
        }
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (balloonSpawner == null)
        {
            Debug.LogError("BalloonSpawner is not assigned in the inspector!");
        }

        if (popSound == null)
        {
            Debug.LogWarning("Pop sound is not assigned in the inspector!");
        }

        if (popEffect == null)
        {
            Debug.LogWarning("Pop particle effect is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 inputPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;

            if (inputPosition.x >= 0 && inputPosition.y >= 0 && inputPosition.x <= Screen.width && inputPosition.y <= Screen.height)
            {
                Ray ray = cam.ScreenPointToRay(inputPosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null && hit.collider.CompareTag("Balloon"))
                {
                    PopBalloon(hit.collider.gameObject);
                }
            }
        }
    }

    void PopBalloon(GameObject balloon)
    {
        if (balloon == null) return;

        balloonSpawner.ReleaseBalloon(balloon);

        if (popSound != null)
        {
            audioSource.PlayOneShot(popSound);
        }

        if (popEffect != null)
        {
            ParticleSystem effect = Instantiate(popEffect, balloon.transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(10);
        }
        else
        {
            Debug.LogError("ScoreManager is not initialized.");
        }
    }

    private void OnDrawGizmos()
    {
        if (cam == null) return;

        Vector3 inputPosition = Input.mousePosition;

        if (inputPosition.x >= 0 && inputPosition.y >= 0 && inputPosition.x <= Screen.width && inputPosition.y <= Screen.height)
        {
            Vector3 worldPosition = cam.ScreenToWorldPoint(inputPosition);
            Gizmos.DrawSphere(worldPosition, 0.1f);
        }
    }
}
