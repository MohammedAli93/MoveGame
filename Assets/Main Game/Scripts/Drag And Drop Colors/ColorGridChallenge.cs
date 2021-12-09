using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorGridChallenge : Challenge
{
    [Header("Components")]
    [SerializeField] List<ColorPicker> colorPickers;
    [SerializeField] List<EmptySlot> exampleSlots;
    [SerializeField] List<EmptySlot> emptySlots;

    [Space(10)]
    [Header("Events")]
    [SerializeField] UnityEvent OnColorPut;
    [SerializeField] UnityEvent OnColorThrown;

    private void Awake()
    {
        foreach (var colorPicker in colorPickers)
        {
            colorPicker.OnPieceThrown += () => OnColorThrown.Invoke();
            colorPicker.OnPiecePut += () => OnColorPut.Invoke();
            colorPicker.OnPiecePut += EvaluateChallenge;
        }
        ToggleChallengeVisibility(false);
    }

    public override void StartChallenge()
    {
        ResetChallenge();
        DisableSlots(exampleSlots);
        ToggleChallengeVisibility(true);
    }

    void ToggleChallengeVisibility(bool value)
    {
        foreach (var item in colorPickers)
        {
            item.gameObject.SetActive(value);
        }
        foreach (var item in exampleSlots)
        {
            item.gameObject.SetActive(value);
        }
        foreach (var item in emptySlots)
        {
            item.gameObject.SetActive(value);
        }
    }

    void EvaluateChallenge()
    {
        if (CheckGrid())
        {
            FinishChallengeWithDelay();
        }
        else
        {
            foreach (var item in emptySlots)
            {
                if (!item.isOcuppied)
                    return;
            }
            FailChallengeWithDelay();
        }
    }

    bool CheckGrid()
    {
        for (int i = 0; i < emptySlots.Count; i++)
        {
            if (exampleSlots[i].transform.childCount == emptySlots[i].transform.childCount)
            {
                if (exampleSlots[i].transform.childCount == 0) continue;
                else
                {
                    Color c1 = exampleSlots[i].transform.GetChild(0).GetComponent<Image>().color;
                    Color c2 = emptySlots[i].transform.GetChild(0).GetComponent<Image>().color;

                    if (c1 != c2) return false;
                }
            }
            else
                return false;
        }
        return true;
    }

    void DisableSlots(List<EmptySlot> slots)
    {
        foreach (var slot in slots)
        {
            slot.isOcuppied = true;
        }
    }

    [ContextMenu("Reset Challenge")]
    public override void ResetChallenge()
    {
        foreach (var item in emptySlots)
        {
            if (item.transform.childCount > 0)
            {
                Destroy(item.transform.GetChild(0).gameObject);
            }
            item.isOcuppied = false;
        }
    }
}