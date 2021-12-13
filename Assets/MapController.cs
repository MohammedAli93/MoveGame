using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] Button Exploration;
    [SerializeField] Button Skill;
    [SerializeField] Button Master;
    [SerializeField] Button Master2;
    [SerializeField] MapItem map2;
    [SerializeField] MapItem map3;
    [SerializeField] MapItem map4;


    public void OnEnable()
    {
        Exploration.image.color = new Color(0, 0, 0, 0);
        Skill.image.color = new Color(0, 0, 0, 0);
        Master.image.color = new Color(0, 0, 0, 0);
        Master2.image.color = new Color(0, 0, 0, 0);

        switch (PlayerPrefs.GetInt("Level", 0))
        {
            case 0:
                ToggleArea(Exploration, 1);
                ToggleArea(Skill, 0);
                ToggleArea(Master, 0);
                ToggleArea(Master2, 0);

                break;
            case 1:
                ToggleArea(Exploration, 1);
                ToggleArea(Skill, 1);
                ToggleArea(Master, 0);
                ToggleArea(Master2, 0);
                map2.Activate();

                break;
            case 2:
                ToggleArea(Exploration, 1);
                ToggleArea(Skill, 1);
                ToggleArea(Master, 1);
                ToggleArea(Master2, 0);
                map2.Activate();
                map3.Activate();

                break;
            case 3:
                ToggleArea(Exploration, 1);
                ToggleArea(Skill, 1);
                ToggleArea(Master, 1);
                ToggleArea(Master2, 1);
                map2.Activate();
                map3.Activate();
                map4.Activate();

                break;
            default:
                ToggleArea(Exploration, 1);
                ToggleArea(Skill, 1);
                ToggleArea(Master, 1);
                ToggleArea(Master2, 1);
                break;
        }
    }

    void ToggleArea(Button area, int value)
    {
        //area.image.color = new Color(area.image.color.r, area.image.color.g, area.image.color.b, 1 - value);
        area.interactable = value == 0 ? false : true;
    }
}