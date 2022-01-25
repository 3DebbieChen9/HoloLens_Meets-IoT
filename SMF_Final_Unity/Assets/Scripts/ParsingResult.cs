using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 public class ParsingResult : MonoBehaviour
{
    [SerializeField]
    private SocketManager socketManager;
    //public string yolo_object = "";
    //public float yolo_accuracy = 0.0f;
    public int motor_pos = 0;
    public bool motor_Do1 = false;
    public bool motor_Do2 = false;
    public bool motor_Do3 = false;
    public bool motor_Do4 = false;
    public bool motor_Di1 = false;
    public bool motor_Di2 = false;
    public bool motor_Di3 = false;
    public bool motor_Di4 = false;
    public bool motor_Di5 = false;
    public bool motor_Di6 = false;
    public TMP_Text yolo_text;
    public GameObject motorStauts;


    // Start is called before the first frame update
    void Start()
    {
        motorStauts.SetActive(false);
        yolo_text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (socketManager.receiveMsg != null || socketManager.receiveMsg != "")
        {
            parsing(socketManager.receiveMsg);
        }
        //else
        //{
        //    motorStauts.SetActive(false);
        //    yolo_text.text = "";
        //}
    }

    void parsing(string message)
    {
        //Debug.Log("Parsing");
        string[] split_yolo_thingworx = message.Split('|');

        if(split_yolo_thingworx.Length == 1) // not motor
        {
            yolo_text.text = split_yolo_thingworx[0];
            //motorStauts.SetActive(false);
            //Debug.Log("not motor");
        }
        else // motor
        {
            // YoloResult
            yolo_text.text = split_yolo_thingworx[0];
            //Debug.Log("motor");
            // MotorStatus
            motorStauts.SetActive(true);
            string[] motorStatus = split_yolo_thingworx[1].Split(',');
            foreach(string status in motorStatus)
            {
                string[] tmp = status.Split('-');
                try
                {
                    switch (tmp[0])
                    {
                        case "Do1":
                            motor_Do1 = bool.Parse(tmp[1]);
                            break;
                        case "Do2":
                            motor_Do2 = bool.Parse(tmp[1]);
                            break;
                        case "Do3":
                            motor_Do3 = bool.Parse(tmp[1]);
                            break;
                        case "Do4":
                            motor_Do4 = bool.Parse(tmp[1]);
                            break;
                        case "Di1":
                            motor_Di1 = bool.Parse(tmp[1]);
                            break;
                        case "Di2":
                            motor_Di2 = bool.Parse(tmp[1]);
                            break;
                        case "Di3":
                            motor_Di3 = bool.Parse(tmp[1]);
                            break;
                        case "Di4":
                            motor_Di4 = bool.Parse(tmp[1]);
                            break;
                        case "Di5":
                            motor_Di5 = bool.Parse(tmp[1]);
                            break;
                        case "Di6":
                            motor_Di6 = bool.Parse(tmp[1]);
                            break;
                        case "Pos":
                            motor_pos = int.Parse(tmp[1]);
                            break;
                        default:
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Debug.Log("Message Format error: " + ex);
                }
            }
        }
    }
}

