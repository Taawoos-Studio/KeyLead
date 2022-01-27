using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public ObjectType type;
    public List<Material> playerMaterial = new List<Material>();
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Inventory inventory;
    
    private bool alreadyToggle = false;

    public void Awake()
    {
        this.skinnedMeshRenderer.material = playerMaterial[(int)type];
    }

    public void FixedUpdate()
    {
        HandleToggle();
    }

    void HandleToggle()
    {
        if(alreadyToggle) {
            switch(type)
            {
                case ObjectType.Black: alreadyToggle = Input.GetButton("Toggle_1"); break;
                case ObjectType.White: alreadyToggle = Input.GetButton("Toggle_2"); break;
            }
        }
        else {
            bool toggle = false;
            switch(type)
            {
                case ObjectType.Black: toggle = Input.GetButton("Toggle_1"); break;
                case ObjectType.White: toggle = Input.GetButton("Toggle_2"); break;
            }
            
            if(toggle)
            {
                inventory.TogglePositive();
                alreadyToggle = true;
            }
        }
    }
}
