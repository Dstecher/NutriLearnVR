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

    // Start is called before the first frame update
    void Start()
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
            if (selectionButton.action.WasPressedThisFrame())
            {
                // Get the FoodProperties first
                FoodProperties foodProperties = gameObject.GetComponent<FoodProperties>();

                // Add the current object to the selection
                if (foodProperties)
                {
                    selectionController.AddItemToSelection(foodProperties);
                }
                else
                {
                    Debug.Log("Something went wrong! Please check the food properties of the current object: " + gameObject.name);
                }
            }
        }
    }
}
