using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProperties : MonoBehaviour
{
    [SerializeField] int kcal; // the amount of kcal for the food product
    [SerializeField] float proteinPer100; // the amount of protein for the food product per 100 g/ml
    [SerializeField] float carbsPer100; // the amount of carbs for the food product per 100 g/ml
    [SerializeField] float fatsPer100; // the amount of fats for the food product per 100 g/ml
    [SerializeField] float weight; // the weight OR volume of the food product in g/ml
    [SerializeField] bool isLiquid; // determines whether the food product is liquid or not (for displaying g/ml correctly)

    private float protein; // the amount of protein for the food product
    private float carbs; // the amount of protein for the food product
    private float fats; // the amount of protein for the food product

    // Start is called before the first frame update
    void Start()
    {
        // TODO: if info per 100 is not null: calculate correct macros for product
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: implement getters for macros
}
