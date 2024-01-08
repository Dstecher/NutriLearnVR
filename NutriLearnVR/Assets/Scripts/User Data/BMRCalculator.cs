using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Calculator class used for calculating the basal metabolic rate and average daily energy consumption based on user information
/// </summary>
public class BMRCalculator : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown genderReference;
    [SerializeField] private TMP_InputField ageReference;
    [SerializeField] private TMP_InputField heightReference;
    [SerializeField] private TMP_InputField weightReference;
    [SerializeField] private TextMeshProUGUI outputTextBMR;
    //[SerializeField] private TextMeshProUGUI outputTextRatio; // commented out because recommending compositions to a user is not advised!
    [SerializeField] private TMP_Dropdown activityReference;

    private float bmrResult = 0;
    private float proteinNeedsPerKG = 0;
    private float proteinNeedsTotal = 0;
    private float proteinPercentageFromTotal = 0;
    private float fatPercentageFromTotal = 0.25f;
    private float fatNeedsTotal = 0;
    private float carbosPercentageFromTotal = 0;
    private float carbosNeedsTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Make sure that all values have an input. Else skip further computations
        if (ageReference.text != "" && heightReference.text != "" && weightReference.text != "")
        {
            // Compute BMR based on: https://www.dge.de/gesunde-ernaehrung/faq/energiezufuhr/ (updated on Dec 2nd 2023)
            // Get proteinNeedsPerKG (especially for adolescents) based on: 10.1038/sj.ejcn.1600977 (deprecated)
            switch(genderReference.value)
            {
                case 0:
                    // diverse gender -> take mean of male and female gender
                    bmrResult = (((0.047f * float.Parse(weightReference.text) + 1.009f - 0.01452f * float.Parse(ageReference.text) + 3.21f) * 239) + ((0.047f * float.Parse(weightReference.text) - 0.01452f * float.Parse(ageReference.text) + 3.21f) * 239)) / 2;
                    if (float.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (float.Parse(ageReference.text) > 17)
                    {
                        proteinNeedsPerKG = 1.05f;
                    }
                    else
                    {
                        proteinNeedsPerKG = 1.18f;
                    }
                    break;
                case 1:
                    // male gender -> use male values
                    bmrResult = (0.047f * float.Parse(weightReference.text) + 1.009f - 0.01452f * float.Parse(ageReference.text) + 3.21f) * 239;
                    if (float.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (float.Parse(ageReference.text) > 17)
                    {
                        proteinNeedsPerKG = 1.09f;
                    }
                    else
                    {
                        proteinNeedsPerKG = 1.22f;
                    }
                    break;
                case 2:
                    // female gender -> use female values
                    bmrResult = (0.047f * float.Parse(weightReference.text) - 0.01452f * float.Parse(ageReference.text) + 3.21f) * 239;
                    if (float.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (float.Parse(ageReference.text) > 17)
                    {
                        proteinNeedsPerKG = 1.01f;
                    }
                    else
                    {
                        proteinNeedsPerKG = 1.14f;
                    }
                    break;
                default:
                    // should not happen under normal circumstances; print error message, but compute diverse formula nontheless to prevent other errors from occurring!
                    Debug.Log("[ERROR] Something went wrong when trying to determine gender! Please check Dropdown selection.");
                    bmrResult = (((0.047f * float.Parse(weightReference.text) + 1.009f - 0.01452f * float.Parse(ageReference.text) + 3.21f) * 239) + ((0.047f * float.Parse(weightReference.text) - 0.01452f * float.Parse(ageReference.text) + 3.21f) * 239)) / 2;
                    if (float.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (float.Parse(ageReference.text) > 17)
                    {
                        proteinNeedsPerKG = 1.05f;
                    }
                    else
                    {
                        proteinNeedsPerKG = 1.18f;
                    }
                    break;
            }

            // Update: 2nd December 2023 -> Adjust BMR based on physical activity levels (using average values from range provided by DGE)
            switch(activityReference.value)
            {
                case 0:
                    bmrResult *= 1.25f;
                    break;
                case 1:
                    bmrResult *= 1.45f;
                    break;
                case 2:
                    bmrResult *= 1.65f;
                    break;
                case 3:
                    bmrResult *= 1.85f;
                    break;
                case 4:
                    bmrResult *= 2.2f;
                    break;
            }

            proteinNeedsTotal = proteinNeedsPerKG * float.Parse(weightReference.text);
            proteinPercentageFromTotal = Mathf.Round((proteinNeedsTotal * 4) / bmrResult * 100) / 100; // 1g protein equals 4 cals; rounded to 2 decimals
            carbosPercentageFromTotal = 1 - proteinPercentageFromTotal - fatPercentageFromTotal;

            carbosNeedsTotal = (bmrResult * carbosPercentageFromTotal) / 4; // 1g carbs equals 4 cals
            fatNeedsTotal = (bmrResult * fatPercentageFromTotal) / 9; // 1g fats equals 9 cals

            outputTextBMR.text = Mathf.RoundToInt(bmrResult).ToString() + "kcal/Tag";
            // commented out because recommending compositions to a user is not advised!
            /*outputTextRatio.text = carbosNeedsTotal.ToString("F1") + " g Kohlenhydrate\n"
                                    + proteinNeedsTotal.ToString("F1") + "g Proteine\n"
                                    + fatNeedsTotal.ToString("F1") + "g Fette";*/
        }
    }

    public float GetUserWeight()
    {
        if (weightReference.text != "")
        {
            return float.Parse(weightReference.text);
        }
        else
        {
            return -1f;
        }
    }

    public float GetUserAge()
    {
        if (ageReference.text != "")
        {
            return float.Parse(ageReference.text);
        }
        else
        {
            return -1f;
        }
    }

    public string GetUserGender()
    {
        switch (genderReference.value)
        {
            case 0:
                return "diverse";
            case 1:
                return "male";
            case 2:
                return "female";
            default:
                return "[ERROR] Error determining gender";
        }
    }

    public float GetUserHeight()
    {
        if (heightReference.text != "")
        {
            return float.Parse(heightReference.text);
        }
        else
        {
            return -1f;
        }
    }

    public float GetCarbRatio()
    {
        return fatPercentageFromTotal;
    }

    public float GetProteinRatio()
    {
        return proteinPercentageFromTotal;
    }

    public float GetFatRatio()
    {
        return fatPercentageFromTotal;
    }

    public float GetBMRResult()
    {
        return Mathf.RoundToInt(bmrResult);
    }
}
