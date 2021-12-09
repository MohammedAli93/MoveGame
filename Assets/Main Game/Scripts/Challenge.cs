using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public abstract class Challenge : MonoBehaviour
{
    [SerializeField] UnityEvent OnChallengeStarted;
    [Tooltip("Time before switching to the next challenge")]
    public float timeBeforeChallengeEnd;
    [Tooltip("Time before failing the challenge")]
    public float timeBeforeChallengeFail;
    [Tooltip("For adding sfx and vfx before the end of the challenge")]
    [SerializeField] UnityEvent OnChallengeAboutToFinish;
    [SerializeField] UnityEvent OnChallengeAboutToFail;
    [HideInInspector] public UnityAction OnChallengeFailed;
    [HideInInspector] public UnityAction OnChallengeFinished;

    protected virtual void OnEnable()
    {
        //if (OnChallengeFinished == null || OnChallengeFinished.GetInvocationList().Length == 0)
        //{
        //    StartChallenge();
        //    return;
        //}
        if (MainComponentsReferenceManager.Instance != null && SceneManager.GetActiveScene().buildIndex != 1)
            PlayerPrefs.SetString("Last Saved", SceneManager.GetActiveScene().buildIndex + "-" + MainComponentsReferenceManager.Instance.ChallengeManager?.progress);

        OnChallengeStarted.Invoke();
    }

    public abstract void StartChallenge();

    public void FinishChallengeWithDelay()
    {
        OnChallengeAboutToFinish.Invoke();
        Invoke("EndChallenge", timeBeforeChallengeEnd);
    }

    [ContextMenu("End Challenge")]
    void EndChallenge()
    {
        OnChallengeFinished?.Invoke();
        Debug.Log("Challenge Completed");
    }

    public void FailChallengeWithDelay()
    {
        OnChallengeAboutToFail.Invoke();
        Invoke("FailChallenge", timeBeforeChallengeEnd);
    }

    [ContextMenu("Fail Challenge")]
    void FailChallenge()
    {
        OnChallengeFailed?.Invoke();
        Debug.Log("Challenge Failed");
    }

    public abstract void ResetChallenge();

    //Test
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndChallenge();
        }
#endif
    }
}
