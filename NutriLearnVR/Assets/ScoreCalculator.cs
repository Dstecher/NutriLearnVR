using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreOutputField;
    [SerializeField] private GameObject selectionCanvasReference;

    private float userProteinPercentage = 0;
    private float userCarbPercentage = 0;
    private float userFatPercentage = 0;
    private int selectionCals = 0;
    private float selectionCarbs = 0;
    private float selectionProtein = 0;
    private float selectionFat = 0;
    private float selectionCarbsRatio = 0;
    private float selectionProteinRatio = 0;
    private float selectionFatRatio = 0;
    private float carbDiff = 0;
    private float proteinDiff = 0;
    private float fatDiff = 0;
    private float score = 0;
    private BMRCalculator bMRCalculator;
    private SelectionCanvasController selectionCanvasController;

    void Awake()
    {
        bMRCalculator = GetComponent<BMRCalculator>();
        selectionCanvasController = selectionCanvasReference.GetComponent<SelectionCanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        userCarbPercentage = bMRCalculator.GetCarbRatio();
        userProteinPercentage = bMRCalculator.GetProteinRatio();
        userFatPercentage = bMRCalculator.GetFatRatio();

        selectionCanvasController.GetScoreValues(out selectionCals, out selectionCarbs, out selectionProtein, out selectionFat);

        selectionCarbsRatio = selectionCarbs * 4 / selectionCals; //1g carbs equals 4 cals
        selectionProteinRatio = selectionProtein * 4 / selectionCals; //1g protein equals 4 cals
        selectionFatRatio = selectionFat * 9 / selectionCals; //1g fat equals 9 cals

        carbDiff = Mathf.Abs(userCarbPercentage - selectionCarbsRatio);
        proteinDiff = Mathf.Abs(userProteinPercentage - selectionProteinRatio);
        fatDiff = Mathf.Abs(userFatPercentage - selectionFatRatio);

        score = 1000 * (1 - (Mathf.Pow((carbDiff), 2) + proteinDiff + fatDiff));

        scoreOutputField.text = $"{score} / 1000 Punkte";
        scoreOutputField.color = Color.Lerp(Color.red, Color.green, score/1000f);
    }

    public float GetCarbDiff()
    {
        return carbDiff;
    }

    public float GetProteinDiff()
    {
        return proteinDiff;
    }

    public float GetFatDiff()
    {
        return fatDiff;
    }
}
