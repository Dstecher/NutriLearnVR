using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Custom class FPS counter for performance test of individual VR headsets. Typically, all related GameObjects are deactivated.
/// </summary>
public class FPSCounter : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI fpsText;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString() + " fps";

            time -= pollingTime;
            frameCount = 0;
        }

        gameObject.transform.rotation = Camera.main.transform.rotation;
        gameObject.transform.position = Camera.main.transform.position;
    }
}
