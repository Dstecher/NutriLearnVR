using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public Transform head;
    [SerializeField] public float menuDistance = 2;
    [SerializeField] public GameObject menuCanvas;

    [SerializeField] public GameObject selectionCanvas;

    [SerializeField] public InputActionProperty showButton;


    // Start is called before the first frame update
    void Start()
    {
        selectionCanvas.gameObject.SetActive(true); // turn on selection canvas on start to keep it inactive during scene building
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }

        menuCanvas.transform.position = head.position + new Vector3(head.forward.x, head.forward.y, head.forward.z).normalized * menuDistance;
        menuCanvas.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        menuCanvas.transform.forward *= -1;
    }
}
