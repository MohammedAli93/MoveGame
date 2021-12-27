using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelGate : MonoBehaviour
{
    public int previousLevel;

    [SerializeField] Sprite active;
    [SerializeField] Sprite inactive;

    Image image => GetComponent<Image>();
    Button button => GetComponent<Button>();
    TMP_Text text => GetComponentInChildren<TMP_Text>();

    private void Start()
    {
        if (PlayerPrefs.GetInt("Entered" + previousLevel, 0) == 1)
        {
            image.sprite = active;
            button.interactable = true;
            text.gameObject.SetActive(true);
        }
        else
        {
            image.sprite = inactive;
            button.interactable = false;
            text.gameObject.SetActive(false);
        }
    }
}