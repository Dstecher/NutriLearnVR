using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for the grab ray of the right hand of the user.
/// </summary>
public class GrabRayController : MonoBehaviour
{

    [SerializeField] private GameObject grabRayReference;
    [SerializeField] private GameObject mainMenuReference;

    private bool showGrabRay = false; // Deactivate this by default, as non-experienced users mentioned higher immersiveness; can be activated for more advanced users by default here.

    public void SwitchGrabRayValue()
    {
        showGrabRay = !showGrabRay;
    }

    // Make sure the grab ray is shown only based on bool value; but always when the main menu is active
    void Update()
    {
        if (mainMenuReference.activeSelf)
        {
            grabRayReference.SetActive(true);
        }
        else
        {
            grabRayReference.SetActive(showGrabRay);
        }
    }
}
