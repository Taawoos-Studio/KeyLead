using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour, IPickable
{
    public ObjectType type;
    public List<Material> mats = new List<Material>();

    public bool TryPick(PlayerBehaviour playerBehaviour)
    {
        if(playerBehaviour.keyCount == 3)
            return false;

        return true;
    }

    void Awake()
    {
        this.GetComponent<MeshRenderer>().material = mats[(int) type];
    }
}
