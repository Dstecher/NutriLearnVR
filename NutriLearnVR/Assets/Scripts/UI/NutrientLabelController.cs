using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NutrientLabelController : MonoBehaviour
{
    // Just a global value to be adjusted by the NutrientInformationManager instances 
    private bool displayLabelGlobal = true;

    public void ChangeDisplayLabelStatus()
    {
        displayLabelGlobal = !displayLabelGlobal;
    }

    public bool GetDisplayLabelStatus()
    {
        return displayLabelGlobal;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MarketSceneReduced")
        {
            displayLabelGlobal = false; // always deactivate NutrientLabels in reduced market scene for user test
        }
    }
}
