using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

/// <summary>
/// Controller class for managing the current user selection of food products
/// </summary>
public class SelectionController : MonoBehaviour
{

    public List<FoodProperties> userSelection; // list to store the current selection in
    [SerializeField] GameObject selectionTable; // table behind the start posititon in the scenes to display the selection on 
    [SerializeField] private GarbageCollector garbageCollectorReference;
    [SerializeField] ConsoleLogger consoleLogger; // only used for user testing

    // Start is called before the first frame update
    void Start()
    {
        userSelection = new List<FoodProperties>();
    }

    public List<FoodProperties> GetUserSelection()
    {
        return userSelection;
    }

    /// <summary>
    /// Add item to selection by adding a copy of given food properties and creating a new instance with the corresponding information. Then remove the item from the scene.
    /// </summary>
    /// <param name="foodProperties"></param>
    public void AddItemToSelection(FoodProperties foodProperties)
    {
        GameObject currentFoodProduct = new GameObject();

        if (selectionTable != null)
        {
            FoodProperties newProperties = foodProperties.CreateCopy(foodProperties, gameObject);
            //userSelection.Add(newProperties);

            // first destroy all childs from the item
            foreach (Transform child in foodProperties.gameObject.transform)
            {
                Destroy(child.gameObject);
            }

            // spawn the item somewhere on (above) the selection table
            // INFO: The spawn range should be somewhere between ([-1.5, 1.5], 2, [-0.6, 0.6])
            currentFoodProduct = Instantiate(foodProperties.gameObject, new Vector3(selectionTable.gameObject.transform.position.x + UnityEngine.Random.Range(-1.5f, 1.5f), selectionTable.gameObject.transform.position.y + 2, selectionTable.gameObject.transform.position.z + UnityEngine.Random.Range(-0.6f, 0.6f)), Quaternion.identity, selectionTable.gameObject.transform) as GameObject;
            currentFoodProduct.GetComponent<Rigidbody>().useGravity = true;
            currentFoodProduct.GetComponent<FoodSelectable>().ChangeSelectionStatus(true);

            userSelection.Add(currentFoodProduct.GetComponent<FoodProperties>());

            consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User has added the following item of category {newProperties.category} to the selection: {newProperties.productName}");

            // despawn the item from the hand
            Destroy(foodProperties.gameObject);
        }
        else
        {
            Debug.Log("[ERROR] Something went wrong when trying to select Food Product. No Selection Table was found!");
        }
    }

    /// <summary>
    /// Used for removing singular food products from the user selection. Removes all instances from the list and deletes all references. Removes the item immediately
    /// </summary>
    /// <param name="foodProperties"></param>
    public void RemoveItemFromSelection(FoodProperties foodProperties)
    {
        foreach (FoodProperties selectedProperties in userSelection)
        {
            if (selectedProperties.id == foodProperties.id)
            {
                selectedProperties.GetComponent<FoodSelectable>().ChangeSelectionStatus(false);
                userSelection.Remove(selectedProperties);
                break;
            }
        }

        FoodProperties[] propertyComponents = GetComponents<FoodProperties>();
        foreach (FoodProperties selectedProperties in propertyComponents)
        {
            if (selectedProperties.id == foodProperties.id)
            {
                Destroy(selectedProperties);
            }
        }

        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User has removed the following item of category {foodProperties.category} to the selection: {foodProperties.productName}");
        
        Destroy(foodProperties.gameObject); // remove the Food Product from the scene as well (should be laying on the referenced selection table)
    }

    /// <summary>
    /// Used for resetting current user selection. Removes all instances from the list and deletes all references.
    /// </summary>
    /// <param name="foodProperties"></param>
    public void RemoveItemFromSelectionForClearing(FoodProperties foodProperties)
    {
        foreach (FoodProperties selectedProperties in userSelection)
        {
            if (selectedProperties.id == foodProperties.id)
            {
                selectedProperties.GetComponent<FoodSelectable>().ChangeSelectionStatus(false);
                userSelection.Remove(selectedProperties);
                break;
            }
        }
        FoodProperties[] propertyComponents = GetComponents<FoodProperties>();
        foreach (FoodProperties selectedProperties in propertyComponents)
        {
            if (selectedProperties.id == foodProperties.id)
            {
                Destroy(selectedProperties);
            }
        }
    }

    /// <summary>
    /// Removes all items form the current user selection by deleting references first and then calling garbage collector to delete objects from scene afterwards.
    /// </summary>
    public void ClearSelection()
    {
        for (int i = userSelection.Count - 1; i >= 0; i--)
        {
            RemoveItemFromSelectionForClearing(userSelection[i]);
        }
        userSelection = new List<FoodProperties>();
        
        garbageCollectorReference.RemoveUnselectedFoodItems();
    }

    // return mean nutri score value for processed food products and counters for other categories
    public void GetScoreInformation(out float nutriScore, out int fruitVegCounter, out int nutsCounter, out int wholeGrainCounter, out int dairyCounter)
    {
        int counterProcessedFood = 0;
        int nutriScoreSum = 0;
        fruitVegCounter = 0;
        nutsCounter = 0;
        wholeGrainCounter = 0;
        dairyCounter = 0;

        foreach (FoodProperties selectionProperties in userSelection)
        {
            if (string.Equals(selectionProperties.category.Trim(), "Obst/Gemüse"))
            {
                fruitVegCounter++;
            }
            else if (string.Equals(selectionProperties.category.Trim(), "Nüsse"))
            {
                nutsCounter++;
            }
            else if (string.Equals(selectionProperties.category.Trim(), "Vollkornprodukte"))
            {
                wholeGrainCounter++;
            }
            else if (string.Equals(selectionProperties.category.Trim(), "Milchprodukte"))
            {
                dairyCounter++;
            }
            else
            {
                nutriScoreSum += selectionProperties.nutriScore;
                counterProcessedFood++;
            }
        }

        if (counterProcessedFood != 0) 
        {
            nutriScore = (float) nutriScoreSum / (float) counterProcessedFood;
        }
        else
        {
            nutriScore = 11; // Initial state --> equals C label, which 0 stars are scored with
        }
    }
}
