using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MatchingChallenge : Challenge
{
    [SerializeField] UnityEvent OnMatching;
    [SerializeField] Color lineColor = Color.white;
    List<MatchingItem> matchingItems = new List<MatchingItem>();
    int MaxProgress;
    int progress = 0;
    private void Awake()
    {
        matchingItems = new List<MatchingItem>(GetComponentsInChildren<MatchingItem>(true));
        foreach (var item in matchingItems)
        {
            item.lineColor = lineColor;
        }
        MaxProgress = matchingItems.Count / 2;
        ToggleChallengeVisibility(false);
    }

    public override void StartChallenge()
    {
        ToggleChallengeVisibility(true);
        MatchingItem.OnMatching2Pieces += IncreaseProgress;
    }

    void ToggleChallengeVisibility(bool value)
    {
        foreach (var piece in matchingItems)
        {
            piece.gameObject.SetActive(value);
        }
    }

    private void OnDisable()
    {
        MatchingItem.OnMatching2Pieces -= IncreaseProgress;
    }

    void IncreaseProgress()
    {
        progress++;
        OnMatching?.Invoke();
        if (progress >= MaxProgress) FinishChallengeWithDelay();
    }

    public override void ResetChallenge()
    {
        throw new System.NotImplementedException();
    }
}
