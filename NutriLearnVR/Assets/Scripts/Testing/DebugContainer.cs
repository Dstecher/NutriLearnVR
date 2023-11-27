using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
