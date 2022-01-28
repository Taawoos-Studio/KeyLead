using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour p = other.GetComponentInParent<PlayerBehaviour>();
        Inventory inv = p.inventory;
        if(inv.keyCount != 3)
            return;

        switch(p.type)
        {
            case ObjectType.Black:
            if(inv.key1 != 1 || inv.key2 != 1 || inv.key3 != 1) return;
            break;
            case ObjectType.White:
            if(inv.key1 != 2 || inv.key2 != 2 || inv.key3 != 2) return;
            break;
        }

        GameManager.winner = (int) p.type;
        GameManager.GoToScene("Win Scene");
    }
}
