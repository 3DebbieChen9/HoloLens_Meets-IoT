using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void StartStream()
    {
        //MediaCaptureUnity.Instance.ToggleVideo();
        SocketManager.Instance.Init();
    }
    
}
