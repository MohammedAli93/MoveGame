using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UndestructableScript : MonoBehaviour
{
    public static UndestructableScript instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        PlayerPrefs.SetInt("just started the game", 1);
        //if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Last Saved", "")))
        //{
        //    string[] splits = PlayerPrefs.GetString("Last Saved", "").Split('-');
        //    PlayerPrefs.SetInt("Sent From Start Screen", 1);
        //    SceneManager.LoadScene(int.Parse(splits[0]));
        //}
    }
}