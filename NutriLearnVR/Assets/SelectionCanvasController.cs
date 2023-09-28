using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class SelectionCanvasController : MonoBehaviour
{
    [SerializeField] private SelectionController selectionController;
    [SerializeField] private TextMeshProUGUI calsText;
    [SerializeField] private TextMeshProUGUI carbsText;
    [SerializeField] private TextMeshProUGUI proteinText;
    [SerializeField] private TextMeshProUGUI fatsText;

    private int calsSum;
    private float carbsSum;
    private float proteinSum;
    private float fatsSum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
        gameObject.transform.position = Camera.main.transform.position;

        UpdateSelectionCanvas();
    }

    /// <summary>
    /// INFO: This method currently is not working fully efficient as it receives and interprets all information every frame instead of only when the selection is updated.
    /// TODO: Fix that
    /// </summary>
    void UpdateSelectionCanvas()
    {
        // Reset sum values to only compute the sums once per selection
        calsSum = 0;
        carbsSum = 0f;
        proteinSum = 0f;
        fatsSum = 0f;

        List<FoodProperties> userSelection = selectionController.GetUserSelection();

        // iterate through the selection and compute the sums
        foreach (FoodProperties foodProperties in userSelection)
        {
            calsSum += foodProperties.calories;
            carbsSum += foodProperties.carbs;
            proteinSum += foodProperties.protein;
            fatsSum += foodProperties.fats;

            //TODO: Make the UI extendable and scalable with content & add all the values from the selection to the text area line-wise
        }

        calsText.text = $"{calsSum}";
        carbsText.text = $"{carbsSum} g";
        proteinText.text = $"{proteinSum} g";
        fatsText.text = $"{fatsSum} g";
    }
}
