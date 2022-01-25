using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using SocketMsgAttributeSpace;
using System.Threading.Tasks;
using System.Threading;

#if !UNITY_EDITOR
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Foundation;
#endif
// GameRoot.cs will set GameManager object active to call this script

public class SocketManager : MonoBehaviour
{
    // Yolo Inferece server
    // ===================================
    public string Server_IP = "140.114.79.246";
    public string receiveMsg = "";

    // Socket Port for HMD send msg to server
    string Port = "8001";
    public CTcpClient Client;
 
    // ===================================
    bool isHandleUI = false;
    bool isAIClientRestart = false;
    public bool isStreaming = false;



    private static SocketManager _instance;
    public static SocketManager Instance
    {
        get
        {
            if(_instance != null)
                return _instance;
            
            _instance = FindObjectOfType<SocketManager>();
            if(_instance != null)
                return _instance;
            
            GameObject singletonObject = new GameObject("Socket Manager");
            _instance = singletonObject.AddComponent<SocketManager>();
            return _instance;
        }
    }

    private void Start() 
    {

    }

    
    private void Update()
    {

        if(isAIClientRestart)
        {
            isAIClientRestart = false;
            StartCoroutine(AI_e_ClientRstart());
        }

        // Start Media Capture when socket connect.
        if(isStreaming)
        {
            MediaCaptureUnity.Instance.ToggleVideo();
            isStreaming = false;
        }
    }

    public void Init()
    {
        // Set inference AI_server IP and ports  
        try
        {
           ClientConnect();
        }
        catch (System.Exception e)
        {
            Debug.Log("init error" + e.Message);
        }
        
    }

    public bool ClientConnect()
	{
        try
        {
            if(Client == null)
            {
                Client = new CTcpClient(Server_IP, int.Parse(Port));
                Client.Receive += ClientReceiveMessage;
                Client.Warning += ClientWarning;
                Client.StartConnect();
            }
            else if(!Client.IsConnected)
            {
                Client.StartConnect();
            }
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log("AIClientConnect error" + e);
            return false;
        }
	}

    private void ClientReceiveMessage(string msg, int length)
	{	
        if (length > 10)
        {
            Debug.Log("Client收到的Real訊息長度：" + length + ", 訊息內容：" + msg);
            receiveMsg = msg;
        }
        else
        {
            Debug.Log("Client收到的Pre訊息長度：" + length + ", 訊息內容：" + msg);
            receiveMsg = msg;
        }
	}

    void ClientWarning()
    {
        Debug.Log("Client transmit Warning"); 
        isAIClientRestart = true; 
        //ClientRestart();
    }

    void ClientRestart()
    {
        if (Client != null)
        {
            try
            {
                Client.Receive -= ClientReceiveMessage;
                Client.Warning -= ClientWarning;
                Client.StopConnect();
                Client = new CTcpClient(Client.IP, Client.Port);
                Client.Receive += ClientReceiveMessage;
                Client.Warning += ClientWarning;
                Client.StartConnect();
            }
            catch (System.Exception e)
            {
                Debug.Log("AIclient restart error = " + e.Message);
                throw;
            }
            
        }
            //StartCoroutine(AI_e_ClientRstart());
    }

    IEnumerator AI_e_ClientRstart()
    {
        Client.Receive -= ClientReceiveMessage;
        Client.Warning -= ClientWarning;
        Client.StopConnect();
        yield return new WaitForSeconds(0.5f);
        Client = new CTcpClient(Client.IP, Client.Port);
        Client.Receive += ClientReceiveMessage;
        Client.Warning += ClientWarning;
        Client.StartConnect();
    }

    void byteLengthToFrameByteArray(string frameInfoStr, byte[] fullBytes)
    {
        Array.Clear(fullBytes, 0, fullBytes.Length);
        byte[] bytesToSendCount = Encoding.ASCII.GetBytes(frameInfoStr);
        bytesToSendCount.CopyTo(fullBytes, 0);
    }

    public void SendJson(object JsonObj)
    {
        try
        {
            if (Client == null)
            {
                Debug.Log("no object!");
                ClientConnect();
                return;
            }
            if (!Client.IsConnected)
            {
                Debug.Log("disconnect!");
                ClientConnect();
                return;
            }
            string registrStr = JsonUtility.ToJson(JsonObj);
            byte[] JsonByte = Encoding.UTF8.GetBytes(registrStr);
            
            //Fill total byte length to send. Result is stored in frameBytesLength
            byte[] frameInfoBytes = new byte[10];
            string frameInfoStr = JsonByte.Length.ToString();
            byteLengthToFrameByteArray(frameInfoStr, frameInfoBytes);

            //Send total byte count first
            Client.SendByte(frameInfoBytes);

            //Send the json bytes
            Client.SendByte(JsonByte);

            //Client.AsyncSendByte(frameInfoBytes, JsonByte);
        }
        catch (System.Exception e)
        {
            Debug.Log("SendToAI_test error" + e.Message);
            throw;
        }
    }

    public void CloseAISocket()
    {
        if (Client!=null)
        {
            Client.Receive -= ClientReceiveMessage;
            Client.Warning -= ClientWarning;
            Client.StopConnect();
            Client = null;
        }
    }

    private void OnDisable() 
    {
        CloseAISocket();
    }
}
