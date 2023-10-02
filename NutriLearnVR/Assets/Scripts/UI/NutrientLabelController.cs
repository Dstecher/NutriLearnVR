using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
