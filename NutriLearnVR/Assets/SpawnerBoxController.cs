using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controller for the SpawnerBox GameObject
/// </summary>
public class SpawnerBoxController : XRBaseInteractable
{
    [SerializeField] GameObject spawnObject;

    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;
    private Transform initiateTransform;

    /*private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("This is the other collider: " + other.gameObject.name);
        if (other.gameObject.tag == "Hand")
        {
            Transform handTransform = other.transform;
            Debug.Log("Instantiating at " + handTransform.position);
            Instantiate(spawnObject, new Vector3(handTransform.position.x, handTransform.position.y + 0.5f, handTransform.position.z), Quaternion.identity);
        }
    }*/

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // first determine, which (hand) transform is closer to the SpawnerBox position to know which hand to instantiate the object in
        Transform boxTransform = gameObject.GetComponent<Transform>();
        float leftDistance = Vector3.Distance(boxTransform.position, leftHandTransform.position);
        float rightDistance = Vector3.Distance(boxTransform.position, rightHandTransform.position);

        Debug.Log($"The left hand distance is {leftDistance}, while the right hand distance is " + rightDistance);

        if (leftDistance < rightDistance)
        {
            initiateTransform = leftHandTransform;
        }
        else
        {
            initiateTransform = rightHandTransform;
        }

        // Instantiate object
        GameObject newObject = Instantiate(spawnObject, initiateTransform.position, Quaternion.identity);
        Debug.Log("Instantiated the object");
        
        // Get grab interactable from prefab
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();
        
        // Select object into same interactor
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);
        Debug.Log("Selected the object");
        
        base.OnSelectEntered(args);
    }
}
