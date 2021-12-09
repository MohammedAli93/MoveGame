using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] float maxBack = 5;
    [SerializeField] float maxFront = 5;
    [SerializeField] float speed = 10;

    private Vector2 original;
    private Vector2 front;
    private Vector2 back;
    private bool isMovingForward = true;

    

    private void Awake()
    {
        original = transform.position;
        front = original + new Vector2(transform.right.x,transform.right.y) * maxFront;
        back = original - new Vector2(transform.right.x, transform.right.y) * maxBack;
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("Level", 0) > 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //float tolerance = 50f * Time.deltaTime;

        //if (isMovingForward)
        //{
        //    if (Vector2.Distance(transform.position, front) <= tolerance)
        //    {
        //        isMovingForward = false;
        //        return;
        //    }
        //    else
        //    {
        //        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        //    }
        //}
        //else
        //{
        //    if (Vector2.Distance(transform.position, back) <= tolerance)
        //    {
        //        isMovingForward = true;
        //        return;
        //    }
        //    else
        //    {
        //        transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        //    }
        //}
    }
}