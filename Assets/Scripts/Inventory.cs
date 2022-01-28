using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int currentIndex = 0;
    public int keyCount = 0;
    public int key1 = -1;
    public int key2 = -1;
    public int key3 = -1;
    public List<GameObject> toggles = new List<GameObject>();
    public List<GameObject> keySlots = new List<GameObject>();
    
    [Header("Sprites")]
    public Sprite blackKeyUI;
    public Sprite whiteKeyUI;
    public Sprite trapUI;

    public void Awake()
    {
        var toggle = this.toggles[currentIndex].transform.Find("Toggle");
        toggle.gameObject.SetActive(true);

        foreach (var slot in toggles)
        {
            if(slot.name == "Key 1") this.keySlots.Insert(0, slot);
            if(slot.name == "Key 2") this.keySlots.Insert(1, slot);
            if(slot.name == "Key 3") this.keySlots.Insert(2, slot);
        }
    }

    public void TogglePositive()
    {
        var toggle = this.toggles[currentIndex].transform.Find("Toggle");
        toggle.gameObject.SetActive(false);

        currentIndex+=1;
        if(currentIndex >= this.toggles.Count)
            currentIndex = 0;
        
        toggle = this.toggles[currentIndex].transform.Find("Toggle");
        toggle.gameObject.SetActive(true);
    }

    public void PickKey(ObjectType type) {
        switch(type) {
            case ObjectType.Black:
                if(key1==-1)
                    AddKeyAt(0, 1);
                else if(key2==-1)
                    AddKeyAt(1, 1);
                else if(key3==-1)
                    AddKeyAt(2, 1);
                break;
            case ObjectType.White:
                if(key1==-1)
                    AddKeyAt(0, 2);
                else if(key2==-1)
                    AddKeyAt(1, 2);
                else if(key3==-1)
                    AddKeyAt(2, 2);
                break;
        }
    }

    void AddKeyAt(int index, int color) {
        switch(index) {
            case 0: key1 = color; break;
            case 1: key2 = color; break;
            case 2: key3 = color; break;
        }
        pSetInventoryKey(index, color);
    }

    void pSetInventoryKey(int index, int color) {
        var imageobj = this.keySlots[index].transform.Find("Image");
        imageobj.gameObject.SetActive(true);
        var image = imageobj.GetComponent<Image>();
        switch (color)
        {
            case 1: image.sprite = blackKeyUI; break;
            case 2: image.sprite = whiteKeyUI; break;
            default: break;
        }
    }

}
