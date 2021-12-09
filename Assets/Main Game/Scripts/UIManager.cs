using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject retryMenuPanel;

    void CloseAllPanels()
    {
        pauseMenuPanel.SetActive(false);
        retryMenuPanel.SetActive(false);
    }

    public void ActivatePauseMenu()
    {
        CloseAllPanels();
        pauseMenuPanel.SetActive(true);
    }

    public void ActivateRetryPanel()
    {
        CloseAllPanels();
        retryMenuPanel.SetActive(true);
    }
}