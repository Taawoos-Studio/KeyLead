using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour, IPickable
{
    public ObjectType type;
    public List<Material> mats = new List<Material>();

    public PickableType pickableType { get => PickableType.Key; }

    public bool TryPick(PlayerBehaviour playerBehaviour)
    {
        if(playerBehaviour.inventory.keyCount == 3)
            return false;

        playerBehaviour.inventory.PickKey(this.type);
        return true;
    }

    void Awake()
    {
        this.GetComponent<MeshRenderer>().material = mats[(int) type];
    }
}
