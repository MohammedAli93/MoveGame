using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HalfAndHalfChallenge : Challenge
{
    [SerializeField] UnityEvent OnPieceConnected;

    private List<PieceBehaviour> pieces = new List<PieceBehaviour>();

    int MaxProgress;

    int progress = 0;

    private void Awake()
    {
        pieces = new List<PieceBehaviour>(GetComponentsInChildren<PieceBehaviour>(true));
        MaxProgress = pieces.Count / 2;        
        ToggleChallengeVisibility(false);
    }

    public override void StartChallenge()
    {
        ToggleChallengeVisibility(true);
        PieceBehaviour.OnAttachingAPiece += IncreaseProgress;
    }

    void ToggleChallengeVisibility(bool value)
    {
        foreach (var piece in pieces)
        {
            piece.gameObject.SetActive(value);
        }
    }

    private void OnDisable()
    {
        PieceBehaviour.OnAttachingAPiece -= IncreaseProgress;
    }

    void IncreaseProgress()
    {
        progress++;
        OnPieceConnected?.Invoke();
        if (progress >= MaxProgress) FinishChallengeWithDelay();
    }

    public override void ResetChallenge()
    {
        throw new System.NotImplementedException();
    }
}
