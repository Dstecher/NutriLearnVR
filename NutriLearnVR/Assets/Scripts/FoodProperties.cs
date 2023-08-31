using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FoodProperties : MonoBehaviour
{
    [SerializeField] public int id; // the for the food product in the database
    [SerializeField] public float weight; // the weight OR volume of the food product in g/ml (handled using the same variable here but entered correspondingly based on specific product model)
    [SerializeField] public string productName; // the name of the food product
    [SerializeField] public int kcalPer100; // the amount of kcal for the food product
    [SerializeField] public float proteinPer100; // the amount of protein for the food product per 100 g/ml
    [SerializeField] public float carbsPer100; // the amount of carbs for the food product per 100 g/ml
    [SerializeField] public float fatsPer100; // the amount of fats for the food product per 100 g/ml
    [SerializeField] public bool isLiquid; // determines whether the food product is liquid or not (for displaying g/ml correctly)

    
    private int calories; // the amount of kcals for the food product
    private float protein; // the amount of protein for the food product
    private float carbs; // the amount of protein for the food product
    private float fats; // the amount of protein for the food product

    // Start is called before the first frame update
    void Start()
    {
        // TODO: if info per 100 is not null: calculate correct macros for product
    }

    /// <summary>
    /// Sets the specific values for the current food product based on the weight and values for 100g/ml stored in the database
    /// </summary>
    public void setSpecificValues()
    {
        if (weight == 0 || kcalPer100 == 0 && proteinPer100 == 0f && carbsPer100 == 0f && fatsPer100 == 0f)
        {
            Debug.Log("[ALERT] Database entries for food product with ID " + id + " were loaded incorrectly!!");
            return;
        }
        
        float weightRatio = weight / 100;

        calories = (int)Math.Round(weightRatio * kcalPer100);
        protein = weightRatio * proteinPer100;
        carbs = weightRatio * carbsPer100;
        fats = weightRatio * fatsPer100;
    }

    public void setName(string pName)
    {
        productName = pName;
    }

    public void setCalories(int kcals)
    {
        kcalPer100 = kcals;
    }

    public void setProtein(float prot)
    {
        proteinPer100 = prot;
    }

    public void setCarbs(float carb)
    {
        carbsPer100 = carb;
    }

    public void setFats(float fat)
    {
        fatsPer100 = fat;
    }

    public void setLiquid(bool liquid)
    {
        isLiquid = liquid;
    }
}
