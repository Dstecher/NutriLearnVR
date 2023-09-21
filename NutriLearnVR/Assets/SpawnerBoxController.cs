using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controller for the SpawnerBox GameObject
/// </summary>
public class SpawnerBoxController : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("This is the other collider: " + other.gameObject.name);
        if (other.gameObject.tag == "Hand")
        {
            Transform handTransform = other.transform;
            Debug.Log("Instantiating at " + handTransform.position);
            Instantiate(spawnObject, new Vector3(handTransform.position.x, handTransform.position.y + 0.5f, handTransform.position.z), Quaternion.identity);
        }
    }
}
