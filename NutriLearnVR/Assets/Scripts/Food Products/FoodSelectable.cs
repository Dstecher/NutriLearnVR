using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodSelectable : MonoBehaviour
{

    public GameObject selectionController;

    private XRGrabInteractable grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        selectionController = GameObject.FindWithTag("UserSelection");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
