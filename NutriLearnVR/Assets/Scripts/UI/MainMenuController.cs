using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public Transform head;
    [SerializeField] GameObject currentCanvas;
    [SerializeField] GameObject mainContentUI;
    [SerializeField] GameObject controlsUI;
    [SerializeField] GameObject userUI;
    [SerializeField] GameObject scoreUI;
    [SerializeField] private Text closeButtonText;
    [SerializeField] GameObject userSelectionReference;
    [SerializeField] GameObject grabRayControllerReference;
    [SerializeField] private UIController uiControllerReference;

    [SerializeField] private ConsoleLogger consoleLogger;

    private GameObject currentlyActiveUI;
    private bool sceneStart = true;

    public void CloseUI()
    {
        if (currentlyActiveUI == mainContentUI)
        {
            if (currentCanvas) currentCanvas.SetActive(false);
        }
        else if (sceneStart)
        {
            if (currentlyActiveUI == userUI)
            {
                currentlyActiveUI.SetActive(false);
                mainContentUI.SetActive(true);
                currentlyActiveUI = mainContentUI;
                sceneStart = false;
                currentCanvas.SetActive(false);
                uiControllerReference.ActivateSelectionCanvas(); // after finishing scene start routine, activate selectionUI 
            }
            if (currentlyActiveUI == controlsUI)
            {
                ShowUserUIInSceneStartRoutine();
            }
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

    public void ShowUserUIInSceneStartRoutine()
    {
        if (controlsUI) controlsUI.SetActive(false);
        if (userUI) userUI.SetActive(true);
        currentlyActiveUI = userUI;
        closeButtonText.text = "←   schließen";
    }

    public void ShowScoreUI()
    {
        if (mainContentUI) mainContentUI.SetActive(false);
        if (scoreUI) scoreUI.SetActive(true);
        currentlyActiveUI = scoreUI;
        closeButtonText.text = "←   zurück";

        consoleLogger.NextTry();
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

    void Start()
    {
        if (sceneStart)
        {
            // if the scene is started initially, start by displaying controls UI
            ShowControlsUI();
        }
    }
}
