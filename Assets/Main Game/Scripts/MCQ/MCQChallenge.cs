using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

public class MCQChallenge : Challenge
{
    [SerializeField] bool isMulti;
    [SerializeField] UnityEvent OnRightAnswer;
    [SerializeField] UnityEvent OnWrongAnswer;

    [SerializeField] GameObject questionGameObject;
    List<MCQChoice> choices = new List<MCQChoice>();

    private void Awake()
    {
        choices = new List<MCQChoice>(GetComponentsInChildren<MCQChoice>(true));

        foreach (var item in choices)
        {
            item.OnChoiceClicked += EvaluateChallenge;
        }

        RandomizeChoicesPositions();

        ToggleChallengeVisibility(false);
    }

    public override void StartChallenge()
    {
        ToggleChallengeVisibility(true);
    }

    void ToggleChallengeVisibility(bool value)
    {
        questionGameObject?.SetActive(value);
        foreach (var choice in choices)
        {
            choice.gameObject.SetActive(value);
        }
    }

    void EvaluateChallenge(MCQChoice choice)
    {
        if (choice.isCorrect)
        {
            if (!isMulti)
            {
                OnRightAnswer.Invoke();
                FinishChallengeWithDelay();
            }
            else
            {
                if (AreAllCorrectChoicesSelected())
                {
                    OnRightAnswer.Invoke();
                    FinishChallengeWithDelay();
                }
            }
        }
        else
        {
            OnWrongAnswer.Invoke();
            FailChallengeWithDelay();
        }
    }

    bool AreAllCorrectChoicesSelected()
    {
        foreach (var item in choices)
        {
            if (item.isCorrect && !item.isSelected)
            {
                return false;
            }
        }
        return true;
    }

    void RandomizeChoicesPositions()
    {
        List<Vector2> positions = new List<Vector2>();

        foreach (var item in choices)
        {
            positions.Add(item.gameObject.transform.position);
        }

        var rnd = new System.Random();
        var randomized = choices.OrderBy(item => rnd.Next()).ToList();

        for (int i = 0; i < randomized.Count; i++)
        {
            randomized[i].transform.position = positions[i];
        }
    }

    public override void ResetChallenge()
    {
        foreach (var item in choices)
        {
            item.isSelected = false;
        }
    }
}
