using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBoxController : MonoBehaviour
{

    [SerializeField] GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"The name of the collider object is {other.gameObject.name}");
    }
}
