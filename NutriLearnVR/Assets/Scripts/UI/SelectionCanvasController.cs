using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectionCanvasController : MonoBehaviour
{
    [SerializeField] private SelectionController selectionController;
    [SerializeField] private TextMeshProUGUI calsText;
    [SerializeField] private TextMeshProUGUI carbsText;
    [SerializeField] private TextMeshProUGUI proteinText;
    [SerializeField] private TextMeshProUGUI fatsText;
    [SerializeField] private TextMeshProUGUI sugarText;
    [SerializeField] private TextMeshProUGUI saturatedFatsText;
    [SerializeField] private TextMeshProUGUI extendedText;

    [SerializeField] private BMRCalculator bmrCalculator;
    [SerializeField] private Gradient colorGradientFatSugar; // full red on threshold, yellow for 80%; green below
    [SerializeField] private Gradient colorGradientProtein; // full red below 0.6g/kg body weight, yellow for <0.8g/kg; green else

    [SerializeField] public InputActionProperty extendedSelectionButton;

    private int calsSum;
    private float carbsSum;
    private float proteinSum;
    private float fatsSum;
    private float sugarSum;
    private float saturatedFatsSum;
    private string extendedString;
    private bool showExtendedText = false;

    private float carbsDiffRatio;
    private float proteinDiffRatio;
    private float fatDiffRatio;
    private float saturatedFatRatio;
    private float sugarRatio;
    private float userWeight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (extendedSelectionButton.action.WasPressedThisFrame()) showExtendedText = !showExtendedText;

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
        // Show extended Selection UI based on bool value by adjusting parent state (UI Sprite element)
        extendedText.transform.parent.gameObject.SetActive(showExtendedText);

        // Reset sum values to only compute the sums once per selection
        calsSum = 0;
        carbsSum = 0f;
        proteinSum = 0f;
        fatsSum = 0f;
        sugarSum = 0f;
        saturatedFatsSum = 0f;
        extendedString = "------------------------------------------------------------------------";

        List<FoodProperties> userSelection = selectionController.GetUserSelection();

        // iterate through the selection and compute the sums
        foreach (FoodProperties foodProperties in userSelection)
        {
            calsSum += foodProperties.calories;
            carbsSum += foodProperties.carbs;
            proteinSum += foodProperties.protein;
            fatsSum += foodProperties.fats;
            sugarSum += foodProperties.sugar;
            saturatedFatsSum += foodProperties.saturatedFat;

            extendedString += $"• {foodProperties.productName}:    {foodProperties.carbs} g ({foodProperties.sugar} g), {foodProperties.protein} g, {foodProperties.fats} g ({foodProperties.saturatedFat} g)\n";
        }

        userWeight = bmrCalculator.GetUserWeight();

        sugarRatio = sugarSum * 4f / calsSum; // 1g sugar equals 4 calories
        saturatedFatRatio = saturatedFatsSum * 9f / calsSum; // 1g saturated sugar equals 9 calories

        // Colorize text fields:
        sugarText.color = colorGradientFatSugar.Evaluate(Mathf.Min((sugarRatio / 0.25f), 1f)); // 25% sugar is the unhealthy threshold after which the color should be red continuously!
        saturatedFatsText.color = colorGradientFatSugar.Evaluate(Mathf.Min((saturatedFatRatio / 0.1f), 1f)); // 10% saturated fat acids is the unhealthy threshold after which the color should be red continuously!
        if (userWeight > 0) proteinText.color = colorGradientProtein.Evaluate(proteinSum / userWeight); // if the user has provided a weight, colorize text based on selected protein in g/kg body weight

        /*
        carbsDiffRatio =  Mathf.Abs((carbsSum / calsSum) - bmrCalculator.GetCarbRatio()) / bmrCalculator.GetCarbRatio();
        proteinDiffRatio =  Mathf.Abs((proteinSum / calsSum) - bmrCalculator.GetProteinRatio()) / bmrCalculator.GetProteinRatio();
        fatDiffRatio =  Mathf.Abs((fatsSum / calsSum) - bmrCalculator.GetFatRatio()) / bmrCalculator.GetFatRatio();

        if (!float.IsNaN(carbsDiffRatio) && !float.IsNaN(proteinDiffRatio) && !float.IsNaN(fatDiffRatio) && SceneManager.GetActiveScene().name != "MarketSceneReduced")
        {
            carbsText.color = colorGradient.Evaluate(carbsDiffRatio);
            proteinText.color = colorGradient.Evaluate(proteinDiffRatio);
            fatsText.color = colorGradient.Evaluate(fatDiffRatio);
        }*/

        calsText.text = $"{calsSum}";
        carbsText.text = $"{carbsSum.ToString("f1")} g";
        proteinText.text = $"{proteinSum.ToString("f1")} g";
        fatsText.text = $"{fatsSum.ToString("f1")} g";
        sugarText.text = $"{sugarSum.ToString("f1")} g";
        saturatedFatsText.text = $"{saturatedFatsSum.ToString("f1")} g";
        extendedText.text = extendedString;
    }

    public void GetScoreValues(out int returnCals, out float returnCarbs, out float returnProtein, out float returnFats)
    {
        returnCals = calsSum;
        returnCarbs = carbsSum;
        returnProtein = proteinSum;
        returnFats = fatsSum;
    }

    public void GetScoreInformation(out float nutriScore, out int fruitVegCounter, out int nutsCounter, out int wholeGrainCounter, out int dairyCounter)
    {
        selectionController.GetScoreInformation(out nutriScore, out fruitVegCounter, out nutsCounter, out wholeGrainCounter, out dairyCounter);
    }

    public int GetUserSelectionLength()
    {
        return selectionController.GetUserSelection().Count;
    }
}
