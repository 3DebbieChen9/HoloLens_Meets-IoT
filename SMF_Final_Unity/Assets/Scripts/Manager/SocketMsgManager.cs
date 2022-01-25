using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SocketMsgAttributeSpace
{

    [System.Serializable]
    public class SMsgAttribute
    {
        public string Real;
    }

    public class SocketMsgManager : MonoBehaviour
    {
        public static SocketMsgManager Instance { get; private set; }
        // =========================================================
        [SerializeField]
        public SMsgAttribute SendMsg = new SMsgAttribute();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ParseJson(string ReceiveMsg)
        {
            try
            {   
                if (ReceiveMsg != null)
                {
                    Debug.Log("Receive msg = " + ReceiveMsg);
                }
                    
            }
            catch (System.Exception e)
            {
                Debug.Log("ParseJson error" + e);
                throw;
            }
        }

    }
}


