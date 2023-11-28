using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] public Transform head;
    [SerializeField] public float menuDistance = 2;
    [SerializeField] public GameObject menuCanvas;

    [SerializeField] public GameObject selectionCanvas;

    [SerializeField] public InputActionProperty showButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);

            if (menuCanvas.activeSelf)
            {
                selectionCanvas.gameObject.SetActive(false); // deactivate selection canvas to reduce overload when main menu is shown
            }
            else
            {
                selectionCanvas.gameObject.SetActive(true); // show selection canvas again
            }
            
            menuCanvas.transform.position = head.position + new Vector3(head.forward.x, head.forward.y, head.forward.z).normalized * menuDistance;
            menuCanvas.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            menuCanvas.transform.forward *= -1;
        }

        //menuCanvas.transform.position = head.position + new Vector3(head.forward.x, head.forward.y, head.forward.z).normalized * menuDistance;
        //menuCanvas.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        //menuCanvas.transform.forward *= -1;
    }

    public void ActivateSelectionCanvas()
    {
        selectionCanvas.gameObject.SetActive(true); // turn on selection canvas (initiated after scene startup routine in MainMenuController)
    }
}
