using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Small class to support display of console messages for easier debugging in builds.
/// </summary>
public class DebugContainer : MonoBehaviour 
{
    public List<string> debugStrings;

    private void Start() {
        debugStrings = new List<string>();
    }

    public void AddDebug(string debugString)
    {
        debugStrings.Add(debugString);
    }
}
