using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class FoodSelectable : MonoBehaviour
{

    public SelectionController selectionController;
    [SerializeField] public InputActionProperty selectionButton;
    [SerializeField] public bool activateDebug = false;

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    public bool isSelected = false; // displays if the current object is selected

    // Start is called before the first frame update
    void Awake()
    {
        grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);

        selectionController = GameObject.FindWithTag("UserSelection").GetComponent<SelectionController>();
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbed = true;
        if (activateDebug) Debug.Log("Object grabbed");
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        isGrabbed = false;
        if (activateDebug) Debug.Log("Object released");
    }

    // Update is called once per frame
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
