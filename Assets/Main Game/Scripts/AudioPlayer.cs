using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    AudioSource audioSource => GetComponent<AudioSource>();

    private void OnEnable()
    {
        audioSource.Play();
    }
}
