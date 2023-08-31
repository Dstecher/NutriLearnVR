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

    [System.Serializable]
    public class FoodProductList
    {
        public FoodProduct[] foodProcuct;
        public int tableSize;
    }

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
        foodProductList.foodProcuct = new FoodProduct[foodProductList.tableSize];

        for (int i = 0; i < foodProductList.tableSize; i++)
        {
            foodProductList.foodProcuct[i] = new FoodProduct();
            if (activateDebug) Debug.Log("[DEBUG] Product ID string: " + data[9 * (i + 1)]);
            foodProductList.foodProcuct[i].id = int.Parse(data[9 * (i + 1)]);
            if (activateDebug) Debug.Log("[DEBUG] Product name string: " + data[9 * (i + 1) + 1]);
            foodProductList.foodProcuct[i].productName = data[9 * (i + 1) + 1];
            if (activateDebug) Debug.Log("[DEBUG] Product cals string: " + data[9 * (i + 1) + 2]);
            foodProductList.foodProcuct[i].cals = float.Parse(data[9 * (i + 1) + 2]);
            if (activateDebug) Debug.Log("[DEBUG] Product fats string: " + data[9 * (i + 1) + 3]);
            foodProductList.foodProcuct[i].fats = float.Parse(data[9 * (i + 1) + 3]);
            if (activateDebug) Debug.Log("[DEBUG] Product carbs string: " + data[9 * (i + 1) + 4]);
            foodProductList.foodProcuct[i].carbs = float.Parse(data[9 * (i + 1) + 4]);
            if (activateDebug) Debug.Log("[DEBUG] Product protein string: " + data[9 * (i + 1) + 5]);
            foodProductList.foodProcuct[i].protein = float.Parse(data[9 * (i + 1) + 5]);

            if (activateDebug) Debug.Log("[DEBUG] Product liquid string: " + data[9 * (i + 1) + 6]);
            if (data[9 * (i + 1) + 6].Contains("ml"))
            {
                foodProductList.foodProcuct[i].isLiquid = true;
            }
            else
            {
                foodProductList.foodProcuct[i].isLiquid = false;
            }
        }
    }

    public bool checkID()
    {
        return true;
    }
}
