using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAndWrongSigns : MonoBehaviour
{
    [SerializeField] GameObject rightSign;
    [SerializeField] GameObject wrongSign;

    private void Awake()
    {
        rightSign.SetActive(false);
        wrongSign.SetActive(false);
    }

    public void ActivateRight(float duration = 1)
    {
        StartCoroutine(ActivateSignForDuration(rightSign, duration));
    }

    public void ActivateWrong(float duration = 1)
    {
        StartCoroutine(ActivateSignForDuration(wrongSign, duration));
    }

    IEnumerator ActivateSignForDuration(GameObject sign,float duration)
    {
        sign.SetActive(true);
        yield return new WaitForSeconds(duration);
        sign.SetActive(false);
    }
}