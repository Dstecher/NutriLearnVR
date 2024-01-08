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

    /// <summary>
    /// ReadCSV() reads data from textAssetData into string array and stores food product information in foodProductList accordingly
    /// </summary>
    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] {";", "\n"}, System.StringSplitOptions.None);

        foodProductList.tableSize = data.Length / 13 - 1;
        foodProductList.foodProduct = new FoodProduct[foodProductList.tableSize];

        for (int i = 0; i < foodProductList.tableSize; i++)
        {
            foodProductList.foodProduct[i] = new FoodProduct();
            if (activateDebug) Debug.Log("[DEBUG] Product ID string: " + data[13 * (i + 1)]);
            foodProductList.foodProduct[i].id = int.Parse(data[13 * (i + 1)]);
            if (activateDebug) Debug.Log("[DEBUG] Product name string: " + data[12 * (i + 1) + 1]);
            foodProductList.foodProduct[i].productName = data[13 * (i + 1) + 1];
            if (activateDebug) Debug.Log("[DEBUG] Product cals string: " + data[12 * (i + 1) + 2]);
            foodProductList.foodProduct[i].cals = int.Parse(data[13 * (i + 1) + 2]);
            if (activateDebug) Debug.Log("[DEBUG] Product fats string: " + data[12 * (i + 1) + 3]);
            foodProductList.foodProduct[i].fats = float.Parse(data[13 * (i + 1) + 3]);
            if (activateDebug) Debug.Log("[DEBUG] Product carbs string: " + data[12 * (i + 1) + 4]);
            foodProductList.foodProduct[i].carbs = float.Parse(data[13 * (i + 1) + 4]);
            if (activateDebug) Debug.Log("[DEBUG] Product protein string: " + data[12 * (i + 1) + 5]);
            foodProductList.foodProduct[i].protein = float.Parse(data[13 * (i + 1) + 5]);

            if (activateDebug) Debug.Log("[DEBUG] Product liquid string: " + data[13 * (i + 1) + 6]);
            if (data[13 * (i + 1) + 6].Contains("ml"))
            {
                foodProductList.foodProduct[i].isLiquid = true;
            }
            else
            {
                foodProductList.foodProduct[i].isLiquid = false;
            }

            if (activateDebug) Debug.Log("[DEBUG] Product saturated fat string: " + data[13 * (i + 1) + 9]);
            //if (data[12 * (i + 1) + 9] != "") foodProductList.foodProduct[i].saturatedFat = float.Parse(data[13 * (i + 1) + 9]);
            float.TryParse(data[13 * (i + 1) + 9], out foodProductList.foodProduct[i].saturatedFat);
            if (activateDebug) Debug.Log("[DEBUG] Product sugar string: " + data[13 * (i + 1) + 10]);
            //if (data[12 * (i + 1) + 10] != "") foodProductList.foodProduct[i].sugar = float.Parse(data[13 * (i + 1) + 10]);
            float.TryParse(data[13 * (i + 1) + 10], out foodProductList.foodProduct[i].sugar);
            if (activateDebug) Debug.Log("[DEBUG] Product Nutri Score string: " + data[13 * (i + 1) + 11]);
            int.TryParse(data[13 * (i + 1) + 11], out foodProductList.foodProduct[i].nutriScore);
            if (activateDebug) Debug.Log("[DEBUG] Category string: " + data[13 * (i + 1) + 12]);
            foodProductList.foodProduct[i].category = data[13 * (i + 1) + 12];
        }
    }

    /// <summary>
    /// Return Function for foodProductList reference
    /// </summary>
    /// <returns>FoodProductList foodProductList</returns>
    public FoodProductList GetFoodProductList()
    {
        return foodProductList;
    }
}
