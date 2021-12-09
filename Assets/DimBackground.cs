using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimBackground : MonoBehaviour
{
    SpriteRenderer sprite => GetComponent<SpriteRenderer>();
    [SerializeField] float dimAlphaValue = 0.4f;

    public void DimBackground1(bool value)
    {
        if (value)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, dimAlphaValue);
        }
        else
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        }
    }
}
