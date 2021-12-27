﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClawCraneButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Animator animator;

    public UnityEvent onClick;

    private Coroutine back;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (back != null)
            return;

        animator.SetTrigger("Pressed");
        onClick.Invoke();
        back = StartCoroutine(Back());
    }

    private IEnumerator Back()
    {
        yield return null;

        animator.SetTrigger("Normal");
        back = null;
    }
}
