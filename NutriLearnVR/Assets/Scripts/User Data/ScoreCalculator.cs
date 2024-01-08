using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] private GameObject selectionCanvasReference;
    [SerializeField] private GameObject scoreUIReference;

    [SerializeField] private TextMeshProUGUI feedbackNutriScore;
    [SerializeField] private TextMeshProUGUI feedbackFruitsVeg;
    [SerializeField] private TextMeshProUGUI feedbackNuts;
    [SerializeField] private TextMeshProUGUI feedbackWholeGrain;
    [SerializeField] private TextMeshProUGUI feedbackDairy;

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
                if (selectionCanvasController.GetUserSelectionLength() == 0) Debug.Log("[INFO] The user has not selected any food product currently");
                DisplayAchievedStarScore(CalculateAchievedStarScore());
            }
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

    public void GetScoreData(out float nutriValue, out int counterFruitVeg, out int counterNuts, out int counterWholeGrain, out int counterDairy, out int currentStarScore)
    {
        currentStarScore = CalculateAchievedStarScore();
        selectionCanvasController.GetScoreInformation(out nutriValue, out counterFruitVeg, out counterNuts, out counterWholeGrain, out counterDairy);
    }

    int CalculateAchievedStarScore()
    {
        int starScore = 0;

        // Get score information from selection canvas controller
        float nutriValue;
        int counterFruitVeg;
        int counterNuts;
        int counterWholeGrain;
        int counterDairy;

        selectionCanvasController.GetScoreInformation(out nutriValue, out counterFruitVeg, out counterNuts, out counterWholeGrain, out counterDairy);

        // return an integer based on the corresponding nutri score value that would heve otherwise been achieved, (including float values here due to average calculation); e.g. A --> 5 stars, B --> 4 stars, ..., E --> 1 star
        if (nutriValue < -0.5)
        {
            starScore++; // give 1 star score for A label
            feedbackNutriScore.color = Color.green;
        }
        else if (nutriValue < 2.5)
        {
            starScore++; // give 1 star score for B label
            feedbackNutriScore.color = Color.green;
        }
        else if (nutriValue < 10.5)
        {
            // no change for C label
        }
        else if (nutriValue < 18.5)
        {
            starScore = starScore - 1; // reduce star score by 1 for D label
            feedbackNutriScore.color = Color.red;
        }
        else
        {
            starScore = starScore - 1; // reduce star score by 1 for E label
            feedbackNutriScore.color = Color.red;
        }

        if (counterFruitVeg >= 5)
        {
            starScore++; // award additional star for at least 5 fruits and vegetables selected
            feedbackFruitsVeg.color = Color.green;
        }
        else
        {
            feedbackFruitsVeg.color = Color.red;
        }

        if (counterNuts >= 1)
        {
            starScore++; // award additional star for at least 1 portion of nuts selected
            feedbackNuts.color = Color.green;
        }
        else
        {
            feedbackNuts.color = Color.red;
        }

        if (counterWholeGrain >= 2)
        {
            starScore++; // award additional star for at least 2 whole grain food products selected
            feedbackWholeGrain.color = Color.green;
        }
        else
        {
            feedbackWholeGrain.color = Color.red;
        }

        if (counterDairy >= 2)
        {
            starScore++; // award additional star for at least 2 dairy food products selected
            feedbackDairy.color = Color.green;
        }
        else
        {
            feedbackDairy.color = Color.red;
        }

        // Deactivate feedback for reduced scene:
        if (SceneManager.GetActiveScene().name == "MarketSceneReduced")
        {
            feedbackNutriScore.gameObject.SetActive(false);
            feedbackFruitsVeg.gameObject.SetActive(false);
            feedbackNuts.gameObject.SetActive(false);
            feedbackWholeGrain.gameObject.SetActive(false);
            feedbackDairy.gameObject.SetActive(false);
        }

        if (starScore < 0)
        {
            return 0; // dont try to display negative star score
        }
        else
        {
            return starScore;
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
