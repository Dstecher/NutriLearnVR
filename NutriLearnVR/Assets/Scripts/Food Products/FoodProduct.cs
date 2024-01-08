using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable class defined with the purpose of defining some properties of food products for the FoodProductList object in the DatabaseReader
/// </summary>
[System.Serializable]
public class FoodProduct
{
    public int id;
    public string productName;
    public int cals;
    public float fats;
    public float carbs;
    public float protein;
    public bool isLiquid;
    public float saturatedFat;
    public float sugar;
    public int nutriScore;
    public string category;
}
