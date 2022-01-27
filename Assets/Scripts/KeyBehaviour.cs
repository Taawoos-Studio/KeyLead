using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public ObjectType type;
    public List<Material> mats = new List<Material>();
    void Awake()
    {
        this.GetComponent<MeshRenderer>().material = mats[(int) type];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
