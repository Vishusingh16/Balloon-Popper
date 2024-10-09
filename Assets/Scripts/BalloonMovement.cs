using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    public float speed = 2f;
    private int movementPattern;

    void OnEnable()
    {
        movementPattern = Random.Range(0, 3); 
    }

    void Update()
    {
        switch (movementPattern)
        {
            case 0:
                MoveStraight();
                break;
            case 1:
                MoveWavy();
                break;
            case 2:
                MoveZigZag();
                break;
        }
    }

    void MoveStraight()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void MoveWavy()
    {
        float wave = Mathf.Sin(Time.time * speed) * 0.5f;
        transform.Translate(new Vector2(wave, 1) * speed * Time.deltaTime);
    }

    void MoveZigZag()
    {
        float zigzag = Mathf.PingPong(Time.time * speed, 1) - 0.5f;
        transform.Translate(new Vector2(zigzag, 1) * speed * Time.deltaTime);
    }
}
