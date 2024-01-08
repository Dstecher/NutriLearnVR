using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Simple class built for packing all wanted information into one string to send it to an address via PUT request
/// </summary>
public class ConsoleLogger : MonoBehaviour
{

    private string sendString = "";

    void Start()
    {
    }

    /// <summary>
    /// Sends the data to the server and does some documentation on the console. Also outputs server response for easier debugging.
    /// </summary>
    public void SendDataToServer()
    {
        string URL3 = ""; // replace this by the address to use
        
        WebRequest requestS3 = (HttpWebRequest) WebRequest.Create(URL3);

        byte[] fileRawBytes = System.Text.Encoding.UTF8.GetBytes(sendString);
        Debug.Log($"[INFO] Packed the following string: {sendString}");
        requestS3.ContentLength = fileRawBytes.Length;

        requestS3.Method = "PUT";

        Stream S3Stream = requestS3.GetRequestStream();
        S3Stream.Write(fileRawBytes, 0, fileRawBytes.Length);

        Debug.Log("[INFO] Sent Message to Server");

        WebResponse myResponse = requestS3.GetResponse();

        Debug.Log(myResponse.ToString());
        
        S3Stream.Close();

        Debug.Log("[INFO] closed s3 stream");
        sendString = "";
    }

    /// <summary>
    /// AppendToSendString() can be publically called from all other classes with a reference to this object. Used for appending new information to the stirng to send after a newline
    /// </summary>
    /// <param name="appendString">string to append to current string to send</param>
    public void AppendToSendString(string appendString)
    {
        sendString += appendString + "\n";
    }
}
