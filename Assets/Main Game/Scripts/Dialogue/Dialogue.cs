using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using RTLTMPro;
using System;

[RequireComponent(typeof(AudioSource))]
public class Dialogue : Challenge
{
    DialogManager dialogManager => MainComponentsReferenceManager.Instance.DialogueManager;

    public List<DialogueLine> lines;
    public RTLTextMeshPro characterNameText;

    private List<DialogData> dialogTexts;

    AudioSource audioSource => GetComponent<AudioSource>();

    private void Awake()
    {
        dialogTexts = new List<DialogData>();

        for (int i = 0; i < lines.Count; i++)
        {
            CharacterEmotion emotion = lines[i].emotion;

            if (i == lines.Count - 1)
            {
                dialogTexts.Add(new DialogData("/emote:" + emotion + "/" + lines[i].line,
                    lines[i].speakerName,
                    () => FinishChallengeWithDelay()));
            }
            else
            {
                string nextName = lines[i + 1].speakerName;
                AudioClip nextAudioClip = lines[i + 1].audioClip;
                dialogTexts.Add(new DialogData("/emote:" + emotion + "/" + lines[i].line,
                    lines[i].speakerName,
                    () =>
                    {
                        characterNameText.text = nextName;
                        audioSource.Stop();
                        audioSource.PlayOneShot(nextAudioClip);
                    }));
            }
        }
    }

    public override void StartChallenge()
    {
        characterNameText.text = lines[0].speakerName;
        audioSource.PlayOneShot(lines[0].audioClip);
        dialogManager.Show(dialogTexts);
    }

    public override void ResetChallenge()
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public struct DialogueLine
{
    public string speakerName;
    [TextArea] public string line;
    public CharacterEmotion emotion;
    public AudioClip audioClip;
}

public enum CharacterEmotion
{
    Normal = 0,
    Sad = 1,
    Happy = 2,
    Worried = 3,
    Smile = 4
}