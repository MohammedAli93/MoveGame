using UnityEngine;

public enum BallColor { Red, Blue, Violet, Yellow };

public class Ball : MonoBehaviour
{
    public BallColor Color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TargetCollider")
        {
            gameObject.SetActive(false);
        }
    }
}

