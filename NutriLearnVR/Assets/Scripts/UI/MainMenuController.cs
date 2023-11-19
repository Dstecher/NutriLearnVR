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
    [SerializeField] GameObject userUI;
    [SerializeField] GameObject scoreUI;
    [SerializeField] private Text closeButtonText;
    [SerializeField] GameObject userSelectionReference;
    [SerializeField] GameObject grabRayControllerReference;

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

    public void ShowUserUI()
    {
        if (mainContentUI) mainContentUI.SetActive(false);
        if (userUI) userUI.SetActive(true);
        currentlyActiveUI = userUI;
        closeButtonText.text = "←   zurück";
    }

    public void ShowScoreUI()
    {
        if (mainContentUI) mainContentUI.SetActive(false);
        if (scoreUI) scoreUI.SetActive(true);
        currentlyActiveUI = scoreUI;
        closeButtonText.text = "←   zurück";
    }

    private void Awake() {
        currentlyActiveUI = mainContentUI;
    }

    public void ResetUserSelection()
    {
        if (userSelectionReference != null) userSelectionReference.GetComponent<SelectionController>().ClearSelection();
    }

    public void SwitchGrabRay()
    {
        if (grabRayControllerReference != null) grabRayControllerReference.GetComponent<GrabRayController>().SwitchGrabRayValue();
    }
}
