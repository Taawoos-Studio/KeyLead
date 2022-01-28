using System;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image onlineStatus;
    static private string serverAddress = "http://127.0.0.1:8000";
    static public string ServerAddress
    {
        get { return serverAddress; }
        set { serverAddress = "http://" + value; }
    }

    HttpWebRequest socket;

    public void Awake()
    {
        // Connect();
    }

    public void Connect()
    {
        this.socket = (HttpWebRequest) WebRequest.Create(ServerAddress);
        this.socket.Method = "POST";
        this.socket.KeepAlive = true;
        using var response = this.socket.GetResponse();
        HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;
        if( statusCode == HttpStatusCode.OK ) {
            this.onlineStatus.color = Color.green;
        }
    }
    
    public void ConnectPlayer(int playerIndex) {
        if(socket != null) {
            Dictionary<string, int> player = new Dictionary<string, int>();
            player["index"] = playerIndex;
            var json = JsonUtility.ToJson(player);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            this.socket.ContentType = "application/x-www-form-urlencoded";
            this.socket.ContentLength = byteArray.Length;

            using var reqStream = this.socket.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = this.socket.GetResponse();
            Debug.Log(((HttpWebResponse)response).StatusDescription); 
        }

        SceneManager.LoadScene("Battle Scene");
    }

    public static void DoActionAfterTime(MonoBehaviour mono, Action action, float time) {

        IEnumerator thing() {
                yield return new WaitForSeconds(time);
                action();
        }
        mono.StartCoroutine(thing());
    }
}
