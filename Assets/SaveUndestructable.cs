using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveUndestructable : MonoBehaviour
{
    public static SaveUndestructable instance;
    [SerializeField] GameObject continueButton;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (PlayerPrefs.GetInt("just started the game", 0) == 1)
        {
            PlayerPrefs.SetInt("just started the game", 0);
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Last Saved", "")))
            {
                continueButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                continueButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void LoadGame()
    {
        string[] splits = PlayerPrefs.GetString("Last Saved", "").Split('-');
        PlayerPrefs.SetInt("Sent From Start Screen", 1);
        SceneManager.LoadScene(int.Parse(splits[0]));
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
    }
}