using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRayController : MonoBehaviour
{

    [SerializeField] private GameObject grabRayReference;
    [SerializeField] private GameObject mainMenuReference;

    private bool showGrabRay = false;

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
