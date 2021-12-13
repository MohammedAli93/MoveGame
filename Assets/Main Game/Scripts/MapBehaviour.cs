using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MapBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text areaOneScore;
    [SerializeField] TMP_Text areaTwoScore;
    [SerializeField] TMP_Text areaThreeScore;
    [SerializeField] TMP_Text areaFourScore;

    [SerializeField] int maxAreaOneScore;
    [SerializeField] int maxAreaTwoScore;
    [SerializeField] int maxAreaThreeScore;
    [SerializeField] int maxAreaFourScore;

    [SerializeField] UnityEvent OnArea1Finished;
    [SerializeField] UnityEvent OnArea2Finished;
    [SerializeField] UnityEvent OnArea3Finished;
    [SerializeField] UnityEvent OnArea4Finished;

    private void Start()
    {
        areaOneScore.text = PlayerPrefs.GetInt("3Score", 0).ToString() + "/" + maxAreaOneScore;
        areaTwoScore.text = PlayerPrefs.GetInt("4Score", 0).ToString() + "/" + maxAreaTwoScore;
        areaThreeScore.text = PlayerPrefs.GetInt("5Score", 0).ToString() + "/" + maxAreaThreeScore;
        areaFourScore.text = PlayerPrefs.GetInt("6Score", 0).ToString() + "/" + maxAreaFourScore;



        if(PlayerPrefs.GetInt("3Score", 0) > maxAreaOneScore / 2)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        if (PlayerPrefs.GetInt("4Score", 0) > maxAreaTwoScore / 2)
        {
            PlayerPrefs.SetInt("Level", 2);
        }
        if (PlayerPrefs.GetInt("5Score", 0) > maxAreaThreeScore / 2)
        {
            PlayerPrefs.SetInt("Level", 3);
        }

        FindObjectOfType<MapController>().OnEnable();

        //if (PlayerPrefs.GetInt("3Celebrate", 0) > 0)
        //{
        //    PlayerPrefs.SetInt("3Celebrate", 0);
        //    OnArea1Finished.Invoke();
        //}
        //else if (PlayerPrefs.GetInt("4Celebrate", 0) > 0)
        //{
        //    PlayerPrefs.SetInt("4Celebrate", 0);
        //    OnArea2Finished.Invoke();
        //}
        //else if (PlayerPrefs.GetInt("5Celebrate", 0) > 0)
        //{
        //    PlayerPrefs.SetInt("5Celebrate", 0);
        //    OnArea3Finished.Invoke();
        //}
    }
}