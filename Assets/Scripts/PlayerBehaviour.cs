using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public ObjectType type;
    public List<Material> playerMaterial = new List<Material>();
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public void Awake()
    {
        this.skinnedMeshRenderer.material = playerMaterial[(int)type];
    }
}
