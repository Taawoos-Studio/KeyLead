using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    string positionUrl = "http://localhost:8000/pos";

    // For Online Game
    void SendPositionPost()
    {
        // var request = WebRequest.Create(positionUrl);
        // request.Method = "POST";

        // var json = JsonUtility.ToJson(transform.position);
        // byte[] byteArray = Encoding.UTF8.GetBytes(json);

        // request.ContentType = "application/x-www-form-urlencoded";
        // request.ContentLength = byteArray.Length;

        // using var reqStream = request.GetRequestStream();
        // reqStream.Write(byteArray, 0, byteArray.Length);

        // using var response = request.GetResponse();
        // Debug.Log(((HttpWebResponse)response).StatusDescription);     
    }
}
