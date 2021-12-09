using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Sequencer : MonoBehaviour
{
    [SerializeField] List<Sequence> sequences;
    [SerializeField] UnityEvent OnSequencesFinished;

    AudioSource audioSource => GetComponent<AudioSource>();

    private void OnEnable()
    {
        StartCoroutine(PlaySequences(sequences));
    }

    IEnumerator PlaySequences(List<Sequence> sequenceList)
    {
        foreach (Sequence sequence in sequenceList)
        {
            for (int i = 0; i < sequence.numberOfTimes; i++)
            {
                audioSource.PlayOneShot(sequence.clip);
                if(sequence.go != null)
                    ActivateSequence(sequenceList.FindIndex(x => x.go == sequence.go));
                yield return new WaitUntil(() => !audioSource.isPlaying);
            }
        }
        DeactivateAllSequences();
        OnSequencesFinished?.Invoke();
        gameObject.SetActive(false);

        void ActivateSequence(int index)
        {
            for (int i = 0; i < sequenceList.Count; i++)
            {
                if(sequenceList[i].go != null)
                    sequenceList[i].go.SetActive(i == index);
            }
        }
        void DeactivateAllSequences()
        {
            foreach (var item in sequenceList)
            {
                if(item.go != null)
                    item.go.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public struct Sequence
{
    public AudioClip clip;
    public GameObject go;
    public int numberOfTimes;
}
