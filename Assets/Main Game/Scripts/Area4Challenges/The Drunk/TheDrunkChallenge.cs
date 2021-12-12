using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDrunkChallenge : Challenge
{
    [SerializeField]
    private TheDrunk _player;
    [SerializeField]
    private int _roundsNumber;
    [SerializeField]
    [Tooltip("In Minutes")]
    private float _roundTime;
    [SerializeField]
    [Tooltip("In Minutes")]
    private float _roundAdditiveTime;

    private int _currentRound;
    private float _currentRoundTime;

    public override void ResetChallenge()
    {
        StartChallenge();
    }

    public override void StartChallenge()
    {
        _currentRound = 0;
        StartRound();
    }

    private void StartRound()
    {
        _currentRoundTime = _currentRound * (_roundAdditiveTime * 60) + (_roundTime * 60);
        _player.Reset();
        _player.StartGame(_currentRoundTime);
    }

    public void EndRound()
    {
        _currentRound++;
        if (_currentRound == _roundsNumber)
            FinishChallengeWithDelay();
        else
            StartRound();
    }
}
