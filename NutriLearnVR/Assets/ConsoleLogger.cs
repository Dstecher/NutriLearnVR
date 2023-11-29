using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConsoleLogger : MonoBehaviour
{
    private string participantID;
    private string filePath;
    private string fileName;
 
    private void Start()
    {
        fileName = "userLogs" + Time.time;
        participantID = PlayerPrefs.GetString("ID", "INVALID");
        filePath = GetFilePath();
    }
 
    private void Update()
    {
        AddRecord("test");
    }
 
    public void AddRecord(string logMessage)
    {
        try
        {
            using (StreamWriter file = new StreamWriter(@filePath, false))
            {
                file.WriteLine($"{Time.time}: {logMessage} \n");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Something went wrong! Error: " + ex.Message);
        }
    }
 
    string GetFilePath()
    {
        return Application.persistentDataPath + "/" + fileName + ".txt";
    }
}
