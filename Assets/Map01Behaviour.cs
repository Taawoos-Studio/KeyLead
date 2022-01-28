using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;

public class Map01Behaviour : MonoBehaviour
{
    public GameObject key;
    Regex asphalt = new Regex(@"^Asphalt");
    List<GameObject> roadblocks = new List<GameObject>();
    void Awake()
    {
        foreach (Transform child in transform) {
            if(asphalt.IsMatch(child.name)) {
                roadblocks.Add(child.gameObject);
            }
        }

        for (int i = 0; i < 3; i++)
            SpawnKey(ObjectType.Black);

        for (int i = 0; i < 3; i++)
            SpawnKey(ObjectType.White);
    }

    void SpawnKey(ObjectType color)
    {
        GameObject block = roadblocks[Random.Range(0, roadblocks.Count)];
        roadblocks.Remove(block);
        var mk = Instantiate(key, block.transform.position+key.transform.position, key.transform.rotation);
        var keyb = mk.GetComponent<KeyBehaviour>();
        keyb.type = color;
        keyb.ResetColor();
    }
}
