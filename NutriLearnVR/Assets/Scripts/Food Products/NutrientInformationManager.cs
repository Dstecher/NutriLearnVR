using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class NutrientInformationManager : MonoBehaviour
{
    [SerializeField] public Transform head;
    [SerializeField] public float verticalCanvasDistance;
    [SerializeField] public GameObject nutrientInformationCanvas;
    [SerializeField] public InputActionProperty showButton;
    [SerializeField] public bool activateDebug = false;
    private bool displayLabel = true;
    private bool labelActive = false;
    private GameObject canvasInstance;
    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);

        if (head == null)
        {
            head = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        // Create Nutrient information label for object
        canvasInstance = Instantiate(nutrientInformationCanvas, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + verticalCanvasDistance, gameObject.transform.position.z), Quaternion.identity, gameObject.transform) as GameObject;
        UpdateNutrientInformation();
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbed = true;
        if (activateDebug) Debug.Log("Object grabbed");
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        isGrabbed = false;
        if (activateDebug) Debug.Log("Object released");
    }

    // Update is called once per frame
    void Update()
    {

        if (showButton.action.WasPressedThisFrame()) displayLabel = !displayLabel;
        if (!displayLabel) return;
        
        if (labelActive == isGrabbed) return;

        if (!labelActive && isGrabbed) labelActive = true;
        if (labelActive && !isGrabbed) labelActive = false;

        canvasInstance.SetActive(labelActive);

        canvasInstance.transform.position = gameObject.transform.position + new Vector3(0, verticalCanvasDistance, 0);
        canvasInstance.transform.LookAt(new Vector3(head.position.x, canvasInstance.transform.position.y, head.position.z));
        canvasInstance.transform.forward *= -1;
    }

    void UpdateNutrientInformation()
    {
        // Get FoodProperties
        FoodProperties foodProperties = gameObject.GetComponent<FoodProperties>();

        TextMeshProUGUI itemNameText = canvasInstance.transform.Find("ItemNameText").gameObject.GetComponent<TextMeshProUGUI>();
        itemNameText.text = foodProperties.productName;

        TextMeshProUGUI itemWeightText = canvasInstance.transform.Find("ItemWeightText").gameObject.GetComponent<TextMeshProUGUI>();
        if (foodProperties.isLiquid)
        {
            itemWeightText.text = $"Volumen pro Stück: ca. {foodProperties.weight.ToString()} ml";
        }
        else
        {
            itemWeightText.text = $"Gewicht pro Stück: ca. {foodProperties.weight.ToString()} g";
        }

        TextMeshProUGUI itemCaloriesSpecific = canvasInstance.transform.Find("ItemCaloriesSpecific").gameObject.GetComponent<TextMeshProUGUI>();
        itemCaloriesSpecific.text = foodProperties.calories.ToString();

        TextMeshProUGUI itemCarbsSpecific = canvasInstance.transform.Find("ItemCarbsSpecific").gameObject.GetComponent<TextMeshProUGUI>();
        itemCarbsSpecific.text = foodProperties.carbs.ToString();

        TextMeshProUGUI itemProteinSpecific = canvasInstance.transform.Find("ItemProteinSpecific").gameObject.GetComponent<TextMeshProUGUI>();
        itemProteinSpecific.text = foodProperties.protein.ToString();

        TextMeshProUGUI itemFatSpecific = canvasInstance.transform.Find("ItemFatSpecific").gameObject.GetComponent<TextMeshProUGUI>();
        itemFatSpecific.text = foodProperties.fats.ToString();

        TextMeshProUGUI itemCaloriesPer100 = canvasInstance.transform.Find("ItemCaloriesPer100").gameObject.GetComponent<TextMeshProUGUI>();
        itemCaloriesPer100.text = foodProperties.kcalPer100.ToString();

        TextMeshProUGUI itemCarbsPer100 = canvasInstance.transform.Find("ItemCarbsPer100").gameObject.GetComponent<TextMeshProUGUI>();
        itemCarbsPer100.text = foodProperties.carbsPer100.ToString();

        TextMeshProUGUI itemProteinPer100 = canvasInstance.transform.Find("ItemProteinPer100").gameObject.GetComponent<TextMeshProUGUI>();
        itemProteinPer100.text = foodProperties.proteinPer100.ToString();

        TextMeshProUGUI itemFatPer100 = canvasInstance.transform.Find("ItemFatPer100").gameObject.GetComponent<TextMeshProUGUI>();
        itemFatPer100.text = foodProperties.fatsPer100.ToString();
    }
}
