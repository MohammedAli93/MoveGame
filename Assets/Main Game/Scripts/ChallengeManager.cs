using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    UIManager UIManager => MainComponentsReferenceManager.Instance.UIManager;

    public UnityEvent OnLevelCompleted = new UnityEvent();

    public List<GameObject> challengesContainers;

    public int currentScore = 0;

    public int maxScore = 53;

    [HideInInspector] public int progress = 0;

    List<Challenge> _challenges;

    [Space(10)]
    [Header("Background")]
    [SerializeField] Image background;
    [SerializeField] float dimAlphaValue = 0.4f;

    [Header("")]
    [SerializeField] public string PlayerPrefStringScore = "3Score";
    [SerializeField] string PlayerPrefStringCelebrate = "3Celebrate";

    private void Start()
    {
        PlayerPrefs.SetInt("Entered" + SceneManager.GetActiveScene().buildIndex, 1);

        _challenges = new List<Challenge>();

        foreach (var item in challengesContainers)
        {
            for (int i = 0; i < item.transform.childCount; i++)
            {
                _challenges.Add(item.transform.GetChild(i).GetComponent<Challenge>());
            }
        }

        foreach (var challenge in _challenges)
        {
            challenge.OnChallengeFinished += OnChallengeFinished;
            challenge.OnChallengeFailed += OnChallengeFailed;
        }

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Last Saved", "")) && PlayerPrefs.GetInt("Sent From Start Screen", 0) == 1)
        {
            PlayerPrefs.SetInt("Sent From Start Screen", 0);
            string[] splits = PlayerPrefs.GetString("Last Saved", "").Split('-');
            if (int.Parse(splits[0]) == SceneManager.GetActiveScene().buildIndex)
            {
                SwitchToChallenge(int.Parse(splits[1]));
            }
        }
        else
        {
            DisableAllChallenges();
            EnableChallenge(progress);
        }
    }

    void DisableAllChallenges()
    {
        foreach (var challenge in _challenges)
        {
            challenge.gameObject.SetActive(false);
        }
    }

    void EnableChallenge(int index)
    {
        _challenges[index].gameObject.SetActive(true);
    }

    public void OnChallengeFinished()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + progress.ToString(), 1);
        SwitchToNextChallenge();
    }

    public void OnChallengeFailed()
    {
        UIManager.ActivateRetryPanel();
    }

    public void SwitchToNextChallenge()
    {
        progress++;

        if (progress >= _challenges.Count)
        {
            OnLevelCompleted.Invoke();
        }
        else
        {
            DisableAllChallenges();
            EnableChallenge(progress);
        }
    }

    public void SwitchToPreviousChallenge()
    {
        progress--;

        if (progress < 0)
        {
            progress = 0;
        }

        DisableAllChallenges();
        EnableChallenge(progress);
    }

    public void SwitchToChallenge(int index)
    {
        progress = index;

        DisableAllChallenges();
        EnableChallenge(progress);
    }

    public void IncrementScore(int amount)
    {
        currentScore += amount;

        //if (currentScore >= maxScore / 2)
        //{
        //    SetLevelPlayerPrefsTo(SceneManager.GetActiveScene().buildIndex - 2);
        //}

        if (currentScore > PlayerPrefs.GetInt("SceneScore " + SceneManager.GetActiveScene().buildIndex, 0))
        {
            PlayerPrefs.SetInt("SceneScore " + SceneManager.GetActiveScene().buildIndex, currentScore);
        }

        CheckScore(amount);
    }

    public Challenge GetCurrentChallenge()
    {
        return progress < _challenges.Count ? _challenges[progress] : null;
    }

    public void RestartCurrentChallenge()
    {
        GetCurrentChallenge()?.ResetChallenge();
    }

    public void SetLevelPlayerPrefsTo(int value)
    {
        PlayerPrefs.SetInt("Level", value);
    }

    void CheckScore(int amount)
    {
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + progress.ToString(), 0) == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefStringScore, PlayerPrefs.GetInt(PlayerPrefStringScore, 0) + amount);
        }


        if (currentScore > PlayerPrefs.GetInt(PlayerPrefStringScore, 0))
        {
            PlayerPrefs.SetInt(PlayerPrefStringScore, currentScore);
            //if (currentScore >= maxScore)
            //{
            //    PlayerPrefs.SetInt(PlayerPrefStringCelebrate, 1);
            //}
        }
    }

    public void DimBackground(bool value)
    {
        if (value)
        {
            background.color = new Color(background.color.r, background.color.g, background.color.b, dimAlphaValue);
        }
        else
        {
            background.color = new Color(background.color.r, background.color.g, background.color.b, 1);
        }
    }
}