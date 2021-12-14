using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    public GameObject LastInteractable;
    public UnityEvent OnHittingBall;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!LastInteractable && coll.gameObject.GetComponent<Ball>())
        {
            LastInteractable = coll.gameObject;
        }

        OnHittingBall.Invoke();
    }
}

