using Doublsb.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComponentsReferenceManager : MonoBehaviour
{
    public static MainComponentsReferenceManager Instance => GetInstanceOfType(_instance);
    public ChallengeManager ChallengeManager => GetInstanceOfType(_challengeManager);
    public DialogManager DialogueManager => GetInstanceOfType(_dialogueManager);
    public UIManager UIManager => GetInstanceOfType(_uiManager);

    private static MainComponentsReferenceManager _instance;
    private ChallengeManager _challengeManager;
    private DialogManager _dialogueManager;
    private UIManager _uiManager;

    private static T GetInstanceOfType<T>(T privateInstance) where T : Component
    {
        if (privateInstance == null) privateInstance = FindObjectOfType<T>();
        return privateInstance;
    }
}
