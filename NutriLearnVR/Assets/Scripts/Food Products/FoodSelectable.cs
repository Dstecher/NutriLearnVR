using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;
using System.Globalization;

/// <summary>
/// This component is being used to give food products the status "isSelected" to display whether an object is currently part of the user selection. The component has to be added to all food products with desired functionality
/// </summary>
public class FoodSelectable : MonoBehaviour
{

    public SelectionController selectionController;
    [SerializeField] public InputActionProperty selectionButton; // Unity Input Action Manager reference with button for event of selecting the food product
    [SerializeField] public bool activateDebug = false;

    [SerializeField] public ConsoleLogger consoleLogger; // reference to a logger used within the user test, not necessary for any functionality

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    public bool isSelected = false; // displays if the current object is selected
    

    /// <summary>
    /// On Awake, set all needed variables and define listeners for Select event in UnityVR
    /// </summary>
    void Awake()
    {
        grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);

        selectionController = GameObject.FindWithTag("UserSelection").GetComponent<SelectionController>();
        consoleLogger = GameObject.FindWithTag("ConsoleLogger").GetComponent<ConsoleLogger>();
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbed = true;
        if (activateDebug) Debug.Log("Object grabbed");
        FoodProperties foodProperties = gameObject.GetComponent<FoodProperties>();
        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User has grabbed food product of category {foodProperties.category} with name: {foodProperties.productName}");
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        isGrabbed = false;
        if (activateDebug) Debug.Log("Object released");
        FoodProperties foodProperties = gameObject.GetComponent<FoodProperties>();
        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User has released food product of category {foodProperties.category} with name: {foodProperties.productName}");
    }

    /// <summary>
    /// Add or remove food product from current user selection based on selection status
    /// </summary>
    void Update()
    {
        if (isGrabbed)
        {

            if (activateDebug) Debug.Log("[DEBUG] The object is currently selected: " + isSelected);

            if (selectionButton.action.WasPressedThisFrame())
            {
                // Get the FoodProperties first
                FoodProperties foodProperties = gameObject.GetComponent<FoodProperties>();

                // Add the current object to the selection
                if (foodProperties)
                {
                    if (!isSelected)
                    {
                        selectionController.AddItemToSelection(foodProperties);
                    }
                    else
                    {
                        selectionController.RemoveItemFromSelection(foodProperties);
                    }
                }
                else
                {
                    Debug.Log("Something went wrong! Please check the food properties of the current object: " + gameObject.name);
                }
            }
        }
    }

    /// <summary>
    /// Change selection status of a food product after calling a corresponding event
    /// </summary>
    /// <param name="status"> the selection status to set the internal value to</param>
    public void ChangeSelectionStatus(bool status)
    {
        isSelected = status; // Typically, this will never have to be set to false as it is the default value when instantiating a new object
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }
}
