using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

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

                // Start Routine had ended: Append user data to logger string for evaluation
                BMRCalculator bMRCalculator = gameObject.GetComponent<BMRCalculator>(); // get reference to BMRCalculator script component
                consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User has closed User UI in start routine with the following information: Gender: {bMRCalculator.GetUserGender()}, Age: {bMRCalculator.GetUserAge()}, Weight: {bMRCalculator.GetUserWeight()}, Height: {bMRCalculator.GetUserHeight()}");
                consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: The user has a total energy consumption of {bMRCalculator.GetBMRResult()} per day on average");
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
        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: Start Routine active in current scene: " + SceneManager.GetActiveScene());
    }

    public void ShowScoreUI()
    {
        if (mainContentUI) mainContentUI.SetActive(false);
        if (scoreUI) scoreUI.SetActive(true);
        currentlyActiveUI = scoreUI;
        closeButtonText.text = "←   zurück";

        ScoreCalculator scoreCalculator = gameObject.GetComponent<ScoreCalculator>();

        float nutriValue;
        int counterFruitVeg;
        int counterNuts;
        int counterWholeGrain;
        int counterDairy;
        int currentStarScore;

        scoreCalculator.GetScoreData(out nutriValue, out counterFruitVeg, out counterNuts, out counterWholeGrain, out counterDairy, out currentStarScore);
        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User checks current score with an average nutri value of {nutriValue}, and total amounts of {counterFruitVeg} fruits/vegetables, {counterNuts} nuts, {counterWholeGrain} whole grain products and {counterDairy} dairy products. This results in a score of {currentStarScore} stars.");

        // Re-confirm user data to make sure whether information was changed in-between:
        BMRCalculator bMRCalculator = gameObject.GetComponent<BMRCalculator>(); // get reference to BMRCalculator script component
        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User information before sending data to server: Gender: {bMRCalculator.GetUserGender()}, Age: {bMRCalculator.GetUserAge()}, Weight: {bMRCalculator.GetUserWeight()}, Height: {bMRCalculator.GetUserHeight()}");
        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: The user has a total energy consumption of {bMRCalculator.GetBMRResult()} per day on average");

        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: Sending data to server");
        consoleLogger.SendDataToServer();
    }

    private void Awake() {
        currentlyActiveUI = mainContentUI;
    }

    public void ResetUserSelection()
    {
        if (userSelectionReference != null) userSelectionReference.GetComponent<SelectionController>().ClearSelection();
        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: The user has reset the whole selection");
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
