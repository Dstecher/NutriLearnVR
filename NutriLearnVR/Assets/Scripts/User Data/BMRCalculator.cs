using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BMRCalculator : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown genderReference;
    [SerializeField] private TMP_InputField ageReference;
    [SerializeField] private TMP_InputField heightReference;
    [SerializeField] private TMP_InputField weightReference;
    [SerializeField] private TextMeshProUGUI outputTextBMR;
    [SerializeField] private TextMeshProUGUI outputTextRatio;

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
            switch(genderReference.value)
            {
                case 0:
                    // diverse gender -> take mean of male and female gender
                    bmrResult = 10 * int.Parse(weightReference.text) + 6.25f * int.Parse(heightReference.text) - 5 * int.Parse(ageReference.text) - 78;
                    if (int.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (int.Parse(ageReference.text) > 17)
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
                    bmrResult = 10 * int.Parse(weightReference.text) + 6.25f * int.Parse(heightReference.text) - 5 * int.Parse(ageReference.text) + 5;
                    if (int.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (int.Parse(ageReference.text) > 17)
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
                    bmrResult = 10 * int.Parse(weightReference.text) + 6.25f * int.Parse(heightReference.text) - 5 * int.Parse(ageReference.text) - 161;
                    if (int.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (int.Parse(ageReference.text) > 17)
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
                    bmrResult = 10 * int.Parse(weightReference.text) + 6.25f * int.Parse(heightReference.text) - 5 * int.Parse(ageReference.text) - 78;
                    if (int.Parse(ageReference.text) < 14)
                    {
                        proteinNeedsPerKG = 1.24f;
                    }
                    else if (int.Parse(ageReference.text) > 17)
                    {
                        proteinNeedsPerKG = 1.05f;
                    }
                    else
                    {
                        proteinNeedsPerKG = 1.18f;
                    }
                    break;
            }

            proteinNeedsTotal = proteinNeedsPerKG * int.Parse(weightReference.text);
            proteinPercentageFromTotal = Mathf.Round((proteinNeedsTotal * 4) / bmrResult * 100) / 100; // 1g protein equals 4 cals; rounded to 2 decimals
            carbosPercentageFromTotal = 1 - proteinPercentageFromTotal - fatPercentageFromTotal;

            carbosNeedsTotal = (bmrResult * carbosPercentageFromTotal) / 4; // 1g carbs equals 4 cals
            fatNeedsTotal = (bmrResult * fatPercentageFromTotal) / 9; // 1g fats equals 9 cals

            outputTextBMR.text = Mathf.RoundToInt(bmrResult).ToString() + "kcal/Tag";
            outputTextRatio.text = carbosNeedsTotal.ToString("F1") + " g Kohlenhydrate\n"
                                    + proteinNeedsTotal.ToString("F1") + "g Proteine\n"
                                    + fatNeedsTotal.ToString("F1") + "g Fette";
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
}
