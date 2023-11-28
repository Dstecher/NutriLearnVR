using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCategoryLabelController : MonoBehaviour
{
    [SerializeField] public Transform head;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        gameObject.transform.forward *= -1;
    }
}
