using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TracingQuestion))]
public class TracingChallenge : Challenge
{
    [SerializeField] List<GameObject> tracingComponents;

    private void Awake()
    {
        ToggleChallengeVisibility(false);
    }

    public override void StartChallenge()
    {
        ToggleChallengeVisibility(true);
    }

    void ToggleChallengeVisibility(bool value)
    {
        foreach (var item in tracingComponents)
        {
            item.SetActive(value);
        }
    }

    public override void ResetChallenge()
    {
        throw new System.NotImplementedException();
    }
}
