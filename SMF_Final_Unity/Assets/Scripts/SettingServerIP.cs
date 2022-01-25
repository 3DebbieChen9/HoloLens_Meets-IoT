using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingServerIP : MonoBehaviour
{
    [SerializeField]
    private GameObject connection;
    [SerializeField]
    private Text serverIP_text;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private SocketManager socketManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIP()
    {
        //socketManager.Server_IP = serverIP_text.text;
        socketManager.Server_IP = "140.114.79.246";
        gameManager.StartStream();
        connection.SetActive(false);
        Debug.Log(serverIP_text.text);
    }
}
