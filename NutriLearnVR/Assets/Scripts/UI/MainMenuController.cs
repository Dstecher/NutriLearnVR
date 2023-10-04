using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject currentCanvas;
    [SerializeField] GameObject mainContentUI;
    [SerializeField] GameObject controlsUI;
    [SerializeField] private Text closeButtonText;

    private GameObject currentlyActiveUI;

    public void CloseUI()
    {
        if (currentlyActiveUI == mainContentUI)
        {
            if (currentCanvas) currentCanvas.SetActive(false);
        }
        else 
        {
            currentlyActiveUI.SetActive(false);
            mainContentUI.SetActive(true);
            currentlyActiveUI = mainContentUI;
            closeButtonText.text = "←   schließen";
        }
    }

    public void ShowControlsUI()
    {
        if (mainContentUI) mainContentUI.SetActive(false);
        if (controlsUI) controlsUI.SetActive(true);
        currentlyActiveUI = controlsUI;
        closeButtonText.text = "←   zurück";
    }

    private void Awake() {
        currentlyActiveUI = mainContentUI;
    }

}
