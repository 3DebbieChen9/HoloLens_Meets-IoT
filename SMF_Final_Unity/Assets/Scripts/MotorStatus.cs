using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MotorStatus : MonoBehaviour
{
    public ParsingResult parsingResult;
    public GameObject posLine;
    public TMP_Text posText;
    public GameObject[] dos;
    public TMP_Text[] dos_texts;
    public GameObject[] dis;
    public TMP_Text[] dis_text;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerRotation = posLine.transform.rotation.eulerAngles;
        posLine.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 90.0f + parsingResult.motor_pos % 4200.0f % 360.0f);
        posText.text = parsingResult.motor_pos.ToString();

        dosUpdate(0, parsingResult.motor_Do1);
        dosUpdate(1, parsingResult.motor_Do2);
        dosUpdate(2, parsingResult.motor_Do3);
        dosUpdate(3, parsingResult.motor_Do4);

        disUpdate(0, parsingResult.motor_Di1);
        disUpdate(1, parsingResult.motor_Di2);
        disUpdate(2, parsingResult.motor_Di3);
        disUpdate(3, parsingResult.motor_Di4);
        disUpdate(4, parsingResult.motor_Di5);
        disUpdate(5, parsingResult.motor_Di6);
    }

    void dosUpdate(int index, bool on)
    {
        Color Do_button_off = new Color(1.0f, 0.7882353f, 0.0f, 0.7843137f);
        Color Do_button_on = new Color(1.0f, 0.07058824f, 0.0f, 0.7843137f);
        if (on)
        {
            dos[index].GetComponent<Image>().color = Do_button_on;
            dos_texts[index].text = "ON";
        }
        else
        {
            dos[index].GetComponent<Image>().color = Do_button_off;
            dos_texts[index].text = "OFF";
        }
    }
    void disUpdate(int index, bool on)
    {
        float Di_button_off = -50.0f;
        float Di_button_on = 50.0f;
        Vector3 eulerRotation = dis[index].transform.rotation.eulerAngles;
        if (on)
        {
            dis[index].transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, Di_button_on);
            dis_text[index].text = "ON";
        }
        else
        {
            dis[index].transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, Di_button_off);
            dis_text[index].text = "OFF";
        }
    }

}
