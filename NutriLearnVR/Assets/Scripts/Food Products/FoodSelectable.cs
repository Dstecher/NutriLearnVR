using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSelectable : MonoBehaviour
{

    public GameObject selectionController;

    private 

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
