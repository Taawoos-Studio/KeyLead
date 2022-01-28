using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject key;
    public GameObject mine;

    [Space]
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

    [Space]
    public PlayerBehaviour player;

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

    public void IndexAction() {
        switch(this.toggles[currentIndex].name)
        {
            case "Key 1":
            if(key1 != -1) {
                player.doingAction = true;
                pPlantKey(key1);
                key1 = -1;
                pUnsetInventoryKey(0);
                this.player.TriggerDrop();
            }
            break;
            case "Key 2":
            if(key2 != -1) {
                player.doingAction = true;
                pPlantKey(key2);
                key2 = -1;
                pUnsetInventoryKey(1);
                this.player.TriggerDrop();
            }
            break;
            case "Key 3":
            if(key3 != -1) {
                player.doingAction = true;
                pPlantKey(key3);
                key3 = -1;
                pUnsetInventoryKey(2);
                this.player.TriggerDrop();
            }
            break;
            case "Trap": break;
            case "Gun": {
                player.Shoot();
            }
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

    void pUnsetInventoryKey(int index) {
        var imageobj = this.keySlots[index].transform.Find("Image");
        imageobj.gameObject.SetActive(false);
    }

    void pPlantKey(int color) {
        switch(color)
        {
            case 1: GameManager.DoActionAfterTime(this, delegate {
                pInitKeyWithColor(ObjectType.Black);
                player.doingAction = false;
            }, player.plantClip.length); break;
            case 2: GameManager.DoActionAfterTime(this, delegate {
                pInitKeyWithColor(ObjectType.White);
                player.doingAction = false;
            }, player.plantClip.length); break;
        }
    }

    void pInitKeyWithColor(ObjectType type) {
        var mKey = Instantiate(key, player.transform.position+(player.transform.forward/3*2), Quaternion.identity);
        mKey.transform.position = new Vector3(mKey.transform.position.x, key.transform.position.y, mKey.transform.position.z);
        mKey.GetComponent<KeyBehaviour>().type = type;
        mKey.GetComponent<KeyBehaviour>().ResetColor();
    }

}
