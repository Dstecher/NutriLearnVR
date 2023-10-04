using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject currentCanvas;
    [SerializeField] GameObject controlsUI;

    public void CloseUI()
    {
        if (currentCanvas) currentCanvas.SetActive(false);
    }

    public void ShowControlsUI()
    {
        if (currentCanvas) currentCanvas.SetActive(false);
        if (controlsUI) controlsUI.SetActive(true);
    }


}
