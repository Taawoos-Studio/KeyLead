using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int currentIndex = 0;
    public List<GameObject> toggles = new List<GameObject>();
    public void Awake()
    {
        var toggle = this.toggles[currentIndex].transform.Find("Toggle");
        toggle.gameObject.SetActive(true);
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

    public void ToggleNegative()
    {
        var toggle = this.toggles[currentIndex].transform.Find("Toggle");
        toggle.gameObject.SetActive(false);

        currentIndex+=1;
        if(currentIndex < 0)
            currentIndex = this.toggles.Count-1;
        
        toggle = this.toggles[currentIndex].transform.Find("Toggle");
        toggle.gameObject.SetActive(true);
    }
}
