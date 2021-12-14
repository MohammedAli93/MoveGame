using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCraneChallenge : Challenge
{
    [SerializeField]
    private ClawCrane _machine;

    public override void ResetChallenge()
    {
        StartChallenge();
    }

    public override void StartChallenge()
    {
        _machine.ResetMachine();
    }
}
