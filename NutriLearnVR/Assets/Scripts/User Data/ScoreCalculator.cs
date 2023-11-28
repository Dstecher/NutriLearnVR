using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] private GameObject selectionCanvasReference;
    [SerializeField] private GameObject scoreUIReference;

    [SerializeField] private Gradient colorGradient;

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
    private float score = 99.0f; // default value that can never be achieved
    private BMRCalculator bMRCalculator;
    private SelectionCanvasController selectionCanvasController;
    private GameObject[] starsEmpty;
    private GameObject[] starsFull;
    private bool foundScoreStars = false;

    void Awake()
    {
        bMRCalculator = GetComponent<BMRCalculator>();
        selectionCanvasController = selectionCanvasReference.GetComponent<SelectionCanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!foundScoreStars)
        {
            GetStarReferences();
        }
        else
        {
            if (selectionCanvasController != null && scoreUIReference.activeSelf)
            {
                if (selectionCanvasController.GetUserSelectionLength() > 0) CalculateNutriScoreBasedUserScore();
                if (score == 99.0f) Debug.Log("[ERROR] There was an error trying to compute the score!");

                Debug.Log("[INFO] The currently achieved score is " + score);
            }

            DisplayAchievedStarScore(CalculateAchievedStarScore());
        }
    }

    void GetStarReferences()
    {
        // If the scoreUI becomes active for the first time, store reference to score star Gameobjects
        if (scoreUIReference.activeSelf && !foundScoreStars)
        {
            starsEmpty = GameObject.FindGameObjectsWithTag("StarEmpty");
            starsFull = GameObject.FindGameObjectsWithTag("StarFull");

            foundScoreStars = true;

            // deactivate all stars
            foreach (GameObject starEmpty in starsEmpty)
            {
                starEmpty.SetActive(false);
            }
            foreach (GameObject starFull in starsFull)
            {
                starFull.SetActive(false);
            }
        }
    }

    void CalculateNutriScoreBasedUserScore()
    {
        score = selectionCanvasController.GetMeanSelectionNutriScore();
    }

    int CalculateAchievedStarScore()
    {
        // return an integer based on the corresponding nutri score value that would heve otherwise been achieved, (including float values here due to average calculation); e.g. A --> 5 stars, B --> 4 stars, ..., E --> 1 star
        if (score < -0.5)
        {
            return 5;
        }
        else if (score < 2.5)
        {
            return 4;
        }
        else if (score < 10.5)
        {
            return 3;
        }
        else if (score < 18.5)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    void DisplayAchievedStarScore(int achievedStars)
    {
        // iterate through max score
        for (int i = 0; i < 5; i++)
        {
            // as long as the currently drawn star is part of the score, draw a full star
            if (i < achievedStars)
            {
                starsFull[i].SetActive(true);
            }
            else
            {
                // draw an empty star to fill up to 5 total stars, as the achieved score is already displayed
                starsEmpty[i].SetActive(true);
            }
        }
    }

    float CalculateOldScorePrototype()
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

        return score;

        /*
        // prevent error messages from early computations
        if (!float.IsNaN(score))
        {
            scoreOutputField.text = score.ToString("F0") + " / 1000 Punkte";

            scoreOutputField.color = colorGradient.Evaluate(score/1000f);
        }
        */
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
