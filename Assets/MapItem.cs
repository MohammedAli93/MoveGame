using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItem : MonoBehaviour
{
    [SerializeField] Sprite activeSprite;
    Image image => GetComponent<Image>();

    public void Activate()
    {
        image.sprite = activeSprite;
    }
}
