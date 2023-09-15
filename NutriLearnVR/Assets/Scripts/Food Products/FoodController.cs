using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    DatabaseReader databaseReader;
    GameObject[] foodProductsInScene;
    FoodProductList foodProductList;

    [SerializeField] public bool activateDebug;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate databaseReader object and get foodProductList object
        databaseReader = gameObject.GetComponent<DatabaseReader>();
        foodProductList = databaseReader.GetFoodProductList();

        // Find all food products in scene
        foodProductsInScene = GameObject.FindGameObjectsWithTag("FoodProduct");

        foreach (GameObject foodProduct in foodProductsInScene)
        {
            if (activateDebug) Debug.Log("[DEBUG] This line is called with the following food product: " + foodProduct.name);
            // Get Properties of current food product
            FoodProperties foodProperties = foodProduct.GetComponent<FoodProperties>();
            if (activateDebug) Debug.Log("[DEBUG] Got the following food properties: " + foodProperties.productName);

            // Make sure that the product exists in database by comparing IDs (due to ID convention this should be equal to index in array)
            if (foodProperties.id > foodProductList.tableSize - 1) return;
            if (foodProductList.foodProcuct[foodProperties.id].id == foodProperties.id)
            {
                if (activateDebug) Debug.Log("[DEBUG] Found food product with ID " + foodProperties.id);
                FoodProduct foodProductDB = foodProductList.foodProcuct[foodProperties.id]; // Store current food product from database

                // Set all necessary data in scene product
                foodProperties.setName(foodProductDB.productName);
                foodProperties.setCalories(foodProductDB.cals);
                foodProperties.setProtein(foodProductDB.protein);
                foodProperties.setCarbs(foodProductDB.carbs);
                foodProperties.setFats(foodProductDB.fats);
                foodProperties.setLiquid(foodProductDB.isLiquid);

                // Compute specific values for the displayed (singular) food product
                foodProperties.setSpecificValues();
            }
            else
            {
                Debug.Log("[ERROR] Something went wrong when comparing IDs. Please re-check for food product with ID " + foodProperties.id);
            }
        }
    }
}
