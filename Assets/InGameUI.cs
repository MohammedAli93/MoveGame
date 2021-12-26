using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TMP_Text score;

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        ResumeGame();
        SceneManager.LoadSceneAsync(4);
    }

    private void Update()
    {
        score.text = PlayerPrefs.GetInt(MainComponentsReferenceManager.Instance.ChallengeManager.PlayerPrefStringScore, 0).ToString("00");
    }
}
