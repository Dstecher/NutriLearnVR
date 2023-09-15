using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    public List<FoodProperties> userSelection;
    [SerializeField] GameObject selectionTable;

    // Start is called before the first frame update
    void Start()
    {
        userSelection = new List<FoodProperties>();
    }

    List<FoodProperties> GetUserSelection()
    {
        return userSelection;
        // TODO: Add specific food product information in the selection UI
    }

    // TODO: Call this function only when the corresponding event is called, e.g. the item is selected and button pressed
    void AddItemToSelection(FoodProperties foodProperties)
    {
        if (selectionTable != null)
        {
            userSelection.Add(foodProperties);

            // spawn the item somewhere on (above) the selection table
            // INFO: The spawn range should be somewhere between ([-1.5, 1.5], 2, [-0.6, 0.6])
            Instantiate(foodProperties.gameObject, new Vector3(selectionTable.gameObject.transform.position.x + Random.Range(-1.5f, 1.5f), selectionTable.gameObject.transform.position.y + 2, selectionTable.gameObject.transform.position.z + Random.Range(-0.6f, 0.6f)), Quaternion.identity, selectionTable.gameObject.transform);

            // despawn the item from the hand
            Destroy(foodProperties.gameObject);
        }
        else
        {
            Debug.Log("[ERROR] Something went wrong when trying to select Food Product. No Selection Table was found!");
        }
    }

    void RemoveItemFromSelection(FoodProperties foodProperties)
    {
        userSelection.Remove(foodProperties);
        Destroy(foodProperties.gameObject); // remove the Food Product from the scene as well (should be laying on the referenced selection table)
    }

    void ClearSelection()
    {
        // INFO: Technically, this could essentially be also achieved by using userSelection.Clear(), however, this would not despawn the items.
        foreach (FoodProperties foodProperties in userSelection)
        {
            RemoveItemFromSelection(foodProperties);
        }
    }
}
