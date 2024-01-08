using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implemented as a performance measure to automatically remove all non-grabbed or non-selected food products from the scene and remove unnecessary rendering of unused products
/// </summary>
public class GarbageCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RemoveUnselectedFoodItems", 30.0f, 15.0f); //Invoke after 30 seconds, every 15 seconds
    }

    public void RemoveUnselectedFoodItems()
    {
        int removedItemsCounter = 0;

        GameObject[] unselectedFoodItems = GameObject.FindGameObjectsWithTag("FoodProduct");

        foreach (GameObject unselectedFoodItem in unselectedFoodItems)
        {
            FoodSelectable foodProduct = unselectedFoodItem.GetComponent<FoodSelectable>();
            if (!foodProduct.IsSelected() && !foodProduct.IsGrabbed())
            {
                // if the food product is neither selected or grabbed currently, remove it from the scene
                Destroy(unselectedFoodItem);
                removedItemsCounter++;
            }
        }
        Debug.Log($"[DEBUG] GarbageCollector Removed {removedItemsCounter} food products from the scene");
    }
}
