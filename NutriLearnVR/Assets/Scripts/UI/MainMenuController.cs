using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject currentCanvas;
    [SerializeField] GameObject mainContentUI;
    [SerializeField] GameObject controlsUI;

    public void CloseUI()
    {
        if (currentCanvas) currentCanvas.SetActive(false);
    }

    public void ShowControlsUI()
    {
        if (mainContentUI) mainContentUI.SetActive(false);
        if (controlsUI) controlsUI.SetActive(true);
    }


}
