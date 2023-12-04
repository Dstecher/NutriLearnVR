using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class SelectionController : MonoBehaviour
{

    public List<FoodProperties> userSelection;
    [SerializeField] GameObject selectionTable;
    [SerializeField] private GarbageCollector garbageCollectorReference;
    [SerializeField] ConsoleLogger consoleLogger;

    // Start is called before the first frame update
    void Start()
    {
        userSelection = new List<FoodProperties>();
    }

    public List<FoodProperties> GetUserSelection()
    {
        return userSelection;
    }

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

        consoleLogger.AppendToSendString($"[TEST] {DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss.ffff", CultureInfo.InvariantCulture)}: User has added the following item of category {foodProperties.category} to the selection: {foodProperties.productName}");
        
        Destroy(foodProperties.gameObject); // remove the Food Product from the scene as well (should be laying on the referenced selection table)
    }

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

    public void ClearSelection()
    {
        // INFO: Technically, this could essentially be also achieved by using userSelection.Clear(), however, this would not despawn the items.
        /**foreach (FoodProperties foodProperties in userSelection)
        {
            RemoveItemFromSelection(foodProperties);
        }*/

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
