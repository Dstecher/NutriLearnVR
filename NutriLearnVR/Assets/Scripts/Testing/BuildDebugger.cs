using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.InputSystem;

public class BuildDebugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI extendedText;
    [SerializeField] private DebugContainer debugContainer;
    [SerializeField] public InputActionProperty extendedSelectionButton;

    private bool showExtendedText = true;
    public string extendedString;

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
        extendedText.transform.parent.gameObject.SetActive(showExtendedText);
        extendedString = "";

        // iterate through the selection and compute the sums
        foreach (string debugString in debugContainer.debugStrings)
        {
            extendedString += $"{debugString}\n";
        }
        extendedText.text = extendedString;
    }
}
