using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WinnerAnnounceText : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        this.textMeshPro = this.GetComponent<TextMeshProUGUI>();
        Debug.Log(GameManager.winner);
        switch(GameManager.winner)
        {
            case 0:
            this.textMeshPro.text = "Black Wins!";
            this.textMeshPro.color = Color.black;
            break;
            case 1: 
            this.textMeshPro.text = "White Wins!";
            this.textMeshPro.color = Color.white;
            break;
            default: break;
        }

        GameManager.DoActionAfterTime(this, delegate {
            GameManager.GoToScene("Battle Scene");
        }, 2);
    }
}
