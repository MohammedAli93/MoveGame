using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteScript : MonoBehaviour
{
    [SerializeField] Sprite UnMuted;
    [SerializeField] Sprite Muted;

    Image image => GetComponent<Image>();

    bool isMuted
    {
        get
        {
            return _isMuted;
        }
        set
        {
            _isMuted = value;
            image.sprite = value ? Muted : UnMuted;
        }
    }

    private bool _isMuted;

    public void ToggleMute()
    {
        isMuted = !isMuted;
        if(UndestructableScript.instance)
            UndestructableScript.instance.GetComponent<AudioSource>().volume = isMuted ? 0 : 0.07f;
    }
}
