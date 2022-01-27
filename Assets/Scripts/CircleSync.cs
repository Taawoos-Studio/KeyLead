using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSync : MonoBehaviour
{
    public static int posID = Shader.PropertyToID("_Position");
    public static int playerPosID = Shader.PropertyToID("_Player_Position");
    public static int sizeID = Shader.PropertyToID("_Size");
    public Material wallMaterial;
    public Camera cam;
    public LayerMask mask;
    // Update is called once per frame
    void Update()
    {
        
        var direction = cam.transform.position - transform.position;
        
        // var ray = new Ray(transform.position, direction.normalized);

        // if(Physics.Raycast(ray, 3000, mask))
        //     wallMaterial.SetFloat(sizeID, 1);
        // else
        //     wallMaterial.SetFloat(sizeID, 0);

        var view = cam.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector(posID, view);
    }
}
