using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseReader : MonoBehaviour
{
    /// <summary>
    /// NOTE: This script was created using the following tutorial: https://www.youtube.com/watch?v=tI9NEm02EuE
    /// </summary>
    public TextAsset textAssetData;
    public bool activateDebug;

    public FoodProductList foodProductList = new FoodProductList();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] {";", "\n"}, System.StringSplitOptions.None);

        foodProductList.tableSize = data.Length / 9 - 1;
        foodProductList.foodProduct = new FoodProduct[foodProductList.tableSize];

        for (int i = 0; i < foodProductList.tableSize; i++)
        {
            foodProductList.foodProduct[i] = new FoodProduct();
            if (activateDebug) Debug.Log("[DEBUG] Product ID string: " + data[9 * (i + 1)]);
            foodProductList.foodProduct[i].id = int.Parse(data[9 * (i + 1)]);
            if (activateDebug) Debug.Log("[DEBUG] Product name string: " + data[9 * (i + 1) + 1]);
            foodProductList.foodProduct[i].productName = data[9 * (i + 1) + 1];
            if (activateDebug) Debug.Log("[DEBUG] Product cals string: " + data[9 * (i + 1) + 2]);
            foodProductList.foodProduct[i].cals = int.Parse(data[9 * (i + 1) + 2]);
            if (activateDebug) Debug.Log("[DEBUG] Product fats string: " + data[9 * (i + 1) + 3]);
            foodProductList.foodProduct[i].fats = float.Parse(data[9 * (i + 1) + 3]);
            if (activateDebug) Debug.Log("[DEBUG] Product carbs string: " + data[9 * (i + 1) + 4]);
            foodProductList.foodProduct[i].carbs = float.Parse(data[9 * (i + 1) + 4]);
            if (activateDebug) Debug.Log("[DEBUG] Product protein string: " + data[9 * (i + 1) + 5]);
            foodProductList.foodProduct[i].protein = float.Parse(data[9 * (i + 1) + 5]);

            if (activateDebug) Debug.Log("[DEBUG] Product liquid string: " + data[9 * (i + 1) + 6]);
            if (data[9 * (i + 1) + 6].Contains("ml"))
            {
                foodProductList.foodProduct[i].isLiquid = true;
            }
            else
            {
                foodProductList.foodProduct[i].isLiquid = false;
            }
        }
    }

    public FoodProductList GetFoodProductList()
    {
        return foodProductList;
    }
}
