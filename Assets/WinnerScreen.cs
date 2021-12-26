using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerScreen : MonoBehaviour
{
    public TMP_Text score;

    private void OnEnable()
    {
        score.text = (PlayerPrefs.GetInt("3Score",0) + PlayerPrefs.GetInt("4Score", 0) + PlayerPrefs.GetInt("5Score", 0)).ToString();
    }
}
