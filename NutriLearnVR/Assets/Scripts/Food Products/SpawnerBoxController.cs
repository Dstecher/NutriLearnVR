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

    private float nextSpawn = 0.1f;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] public bool activateDebug = false;
    [SerializeField] private DebugContainer debugContainer;

    private int counter = 0;

    // INFO: For some reason, the event is called twice in most cases. To prevent this, a cooldown was implemented
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (Time.time < nextSpawn) return; // cancel function call if cooldown has not passed

        // first determine, which (hand) transform is closer to the SpawnerBox position to know which hand to instantiate the object in
        Transform boxTransform = gameObject.GetComponent<Transform>();
        float leftDistance = Vector3.Distance(boxTransform.position, leftHandTransform.position);
        float rightDistance = Vector3.Distance(boxTransform.position, rightHandTransform.position);

        if (activateDebug) Debug.Log($"The left hand distance is {leftDistance}, while the right hand distance is " + rightDistance);
        if (activateDebug) debugContainer.AddDebug($"The left hand distance is {leftDistance}, while the right hand distance is " + rightDistance);

        if (leftDistance < rightDistance)
        {
            initiateTransform = leftHandTransform;
        }
        else
        {
            initiateTransform = rightHandTransform;
        }

        // Instantiate object
        // INFO: The debugs here are mainly used to understand issues with duplicate instantiations happenning randomly from time to time; they seem to be sometimes fixed by application restarts
        if (activateDebug) Debug.Log($"The counter before the instantiate is {counter}.");
        if (activateDebug) debugContainer.AddDebug($"The counter before the instantiate is {counter}.");
        GameObject newObject = Instantiate(spawnObject, initiateTransform.position, Quaternion.identity);
        counter++;
        if (activateDebug) Debug.Log("Instantiated the object");
        if (activateDebug) debugContainer.AddDebug("Instantiated the object");
        if (activateDebug) Debug.Log($"The counter after the instantiate is {counter}.");
        if (activateDebug) debugContainer.AddDebug($"The counter after the instantiate is {counter}.");
        
        // Get grab interactable from prefab
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();
        
        // Select object into same interactor
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);
        if (activateDebug) Debug.Log("Selected the object");
        if (activateDebug) debugContainer.AddDebug("Selected the object");

        nextSpawn = Time.time + spawnDelay; // update cooldown
        
        base.OnSelectEntered(args);
    }
}
