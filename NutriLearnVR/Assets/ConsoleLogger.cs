using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public class ConsoleLogger : MonoBehaviour
{

    private string sendString = "";

    void Start()
    {
    }

    public void NextTry()
    {
        string URL3 = "https://www.sse-testcenter.org/unity/upload";
        
        WebRequest requestS3 = (HttpWebRequest) WebRequest.Create(URL3);

        byte[] fileRawBytes = System.Text.Encoding.UTF8.GetBytes(sendString);
        Debug.Log($"Packed the following string: {sendString}");
        requestS3.ContentLength = fileRawBytes.Length;

        requestS3.Method = "PUT";

        Stream S3Stream = requestS3.GetRequestStream();
        S3Stream.Write(fileRawBytes, 0, fileRawBytes.Length);

        Debug.Log("Sent Message to SSE");

        WebResponse myResponse = requestS3.GetResponse();

        Debug.Log(myResponse.ToString());
        
        S3Stream.Close();

        Debug.Log("closed s3 stream");
    }

    

    void Update()
    {
        AppendToSendString("test" + System.DateTime.UtcNow.ToString() + "\n");
    }

    public void AppendToSendString(string appendString)
    {
        sendString += appendString;
    }
}
