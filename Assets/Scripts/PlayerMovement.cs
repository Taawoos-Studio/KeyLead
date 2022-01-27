using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Vector2 input;
    public Transform camPivot;
    private PlayerBehaviour playerBehaviour;

    private string hValue;
    private string vValue;

    float heading = 0;
    string positionUrl = "http://localhost:8000/pos";
    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.playerBehaviour = GetComponent<PlayerBehaviour>();

        if(playerBehaviour.type == ObjectType.Black) {
            hValue = "Horizontal_1";
            vValue = "Vertical_1";
        }
        if(playerBehaviour.type == ObjectType.White) {
            hValue = "Horizontal_2";
            vValue = "Vertical_2";
        }
    }

    void FixedUpdate()
    {
        heading += Input.GetAxis(hValue)*Time.fixedDeltaTime*180;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);
        this.input = new Vector2(Input.GetAxis(hValue), Input.GetAxis(vValue));
        this.input = Vector2.ClampMagnitude(input, 1);

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized;
        this.GetComponent<Animator>().SetFloat("speed", movement.magnitude);
        
        if(movement == Vector3.zero)
            return;
        

        transform.rotation = Quaternion.LookRotation(movement);
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    // For Online Game
    void SendPositionPost()
    {
        var request = WebRequest.Create(positionUrl);
        request.Method = "POST";

        var json = JsonUtility.ToJson(transform.position);
        byte[] byteArray = Encoding.UTF8.GetBytes(json);

        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;

        using var reqStream = request.GetRequestStream();
        reqStream.Write(byteArray, 0, byteArray.Length);

        using var response = request.GetResponse();
        Debug.Log(((HttpWebResponse)response).StatusDescription);     
    }
}
