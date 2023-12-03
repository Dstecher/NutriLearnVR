using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FoodProperties : MonoBehaviour
{
    [SerializeField] public FoodController foodController;

    [SerializeField] public int id; // the for the food product in the database
    [SerializeField] public float weight; // the weight OR volume of the food product in g/ml (handled using the same variable here but entered correspondingly based on specific product model)
    [SerializeField] public string productName; // the name of the food product
    [SerializeField] public int kcalPer100; // the amount of kcal for the food product
    [SerializeField] public float proteinPer100; // the amount of protein for the food product per 100 g/ml
    [SerializeField] public float carbsPer100; // the amount of carbs for the food product per 100 g/ml
    [SerializeField] public float fatsPer100; // the amount of fats for the food product per 100 g/ml
    [SerializeField] public bool isLiquid; // determines whether the food product is liquid or not (for displaying g/ml correctly)
    [SerializeField] public float saturatedFatPer100; // the amount of saturated fat for the food product per 100 g/ml
    [SerializeField] public float sugarPer100; // the amount of sugar for the food product per 100 g/ml
    [SerializeField] public int nutriScore; // the nutri score that was calculated for the given food product
    [SerializeField] public string category; // the nutri score that was calculated for the given food product

    
    public int calories; // the amount of kcals for the food product
    public float protein; // the amount of protein for the food product
    public float carbs; // the amount of protein for the food product
    public float fats; // the amount of protein for the food product
    public float saturatedFat; // the amount of saturated fat for the food product
    public float sugar; // the amount of sugar for the food product

    /// <summary>
    /// Sets the specific values for the current food product based on the weight and values for 100g/ml stored in the database
    /// </summary>
    public void setSpecificValues()
    {
        if (weight == 0 || kcalPer100 == 0 && proteinPer100 == 0f && carbsPer100 == 0f && fatsPer100 == 0f)
        {
            //TODO: This line is sometimes called after selecting an object for some reason; even though the values for the NutrientLabels are filled correctly.
            // Debug.Log("[ALERT] Database entries for food product with ID " + id + " were loaded incorrectly!!"); // commented out since some calls were wrong here and there was not enough time to fix these calls
            return;
        }
        
        float weightRatio = weight / 100;

        calories = (int)Math.Round(weightRatio * kcalPer100);
        protein = Mathf.Floor(weightRatio * proteinPer100 * 10) / 10;
        carbs = Mathf.Floor(weightRatio * carbsPer100 * 10) / 10;
        fats = Mathf.Floor(weightRatio * fatsPer100 * 10) / 10;
        saturatedFat = Mathf.Floor(weightRatio * saturatedFatPer100 * 10) / 10;
        sugar = Mathf.Floor(weightRatio * sugarPer100 * 10) / 10;
    }

    public void Awake()
    {
        // Set the FoodController reference
        foodController = GameObject.Find("Items/FoodController").GetComponent<FoodController>();

        FoodProduct foodProduct = foodController.getFoodProductByID(id);

        if (foodProduct == null) return; // This means that Awake was called after loading the scene; The intended use is after manual instantiate calls

        setName(foodProduct.productName);
        setCalories(foodProduct.cals);
        setProtein(foodProduct.protein);
        setCarbs(foodProduct.carbs);
        setFats(foodProduct.fats);
        setLiquid(foodProduct.isLiquid);
        setSaturatedFat(foodProduct.saturatedFat);
        setSugar(foodProduct.sugar);
        setNutriScore(foodProduct.nutriScore);
        setCategory(foodProduct.category);

        // Compute specific values for the displayed (singular) food product
        setSpecificValues();
    }

    public void setName(string pName)
    {
        productName = pName;
    }

    public void setCategory(string productCategory)
    {
        category = productCategory;
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

    public void setSaturatedFat(float saturatedFat)
    {
        saturatedFatPer100 = saturatedFat;
    }

    public void setSugar(float sugar)
    {
        sugarPer100 = sugar;
    }

    public void setNutriScore(int score)
    {
        nutriScore = score;
    }

    public FoodProperties CreateCopy(FoodProperties foodProperties, GameObject attachParent)
    {
        FoodProperties tempProperties = attachParent.AddComponent<FoodProperties>();

        tempProperties.id = foodProperties.id;
        tempProperties.weight = foodProperties.weight;
        tempProperties.productName = foodProperties.productName;
        tempProperties.kcalPer100 = foodProperties.kcalPer100;
        tempProperties.proteinPer100 = foodProperties.proteinPer100;
        tempProperties.carbsPer100 = foodProperties.carbsPer100;
        tempProperties.fatsPer100 = foodProperties.fatsPer100;
        tempProperties.isLiquid = foodProperties.isLiquid;
        tempProperties.saturatedFatPer100 = foodProperties.saturatedFatPer100;
        tempProperties.sugarPer100 = foodProperties.sugarPer100;
        tempProperties.nutriScore = foodProperties.nutriScore;

        // For the following specific properties, getters/setters have to be used in case they are made private again
        tempProperties.calories = foodProperties.calories;
        tempProperties.protein = foodProperties.protein;
        tempProperties.carbs = foodProperties.carbs;
        tempProperties.fats = foodProperties.fats;
        tempProperties.saturatedFat = foodProperties.saturatedFat;
        tempProperties.sugar = foodProperties.sugar;
        tempProperties.category = foodProperties.category;

        return tempProperties;
    }

}
