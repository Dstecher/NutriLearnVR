using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Supporting class to store all food product information that can be found within the database
/// </summary>
[System.Serializable]
public class FoodProductList
{
    public FoodProduct[] foodProduct;
    public int tableSize;
}
