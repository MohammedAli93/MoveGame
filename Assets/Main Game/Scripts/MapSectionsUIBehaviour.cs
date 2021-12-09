using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapSectionsUIBehaviour : MonoBehaviour
{
    [SerializeField] List<TMP_Text> scores;
    [SerializeField] List<int> maxScores;
    [SerializeField] int startSceneIndex;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);
        
        for (int i = 0; i < 6; i++)
        {
            scores[i].SetText(PlayerPrefs.GetInt("SceneScore " + (i + startSceneIndex), 0).ToString() + "/" + maxScores[i].ToString());
        }
    }
}