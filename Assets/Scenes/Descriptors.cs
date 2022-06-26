using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Descriptors : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector3 forceVec;
    public GameObject descrptor1;
    public GameObject descrptor2;
    public GameObject descrptor3;
    public Image panelx;
    public Image panely;
    public Image panelz;
    float tiltx;
    float tilty;
    float tiltz;
    float accelx;
    float accely;
    float accelz;
    bool pressed = false;

    

    public Color myColor;
    


    public void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            
        }

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button is pressed");
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Button is released");
        pressed = false;
    }
    
    public void Descriptor()
    {
        if (SystemInfo.supportsGyroscope)
        {

            // tilt from 0 to 360 degrees
            tiltx = (Input.gyro.attitude.eulerAngles.x);
            tilty = (Input.gyro.attitude.eulerAngles.y);
            tiltz = (Input.gyro.attitude.eulerAngles.z);

            //accel from 0 to maybe 3? at max 7 g or m/s^2
            accelz = (Input.gyro.userAcceleration.z);
            accely = (Input.gyro.userAcceleration.y);
            accelx = (Input.gyro.userAcceleration.x);

            // Unity takes in the color API values from 0 to 1
            float colcodey = Mathf.Abs((float)tilty / (float)360);
            float colcodex = Mathf.Abs((float)tiltx / (float)360);
            float colcodez = Mathf.Abs((float)tiltz / (float)360);
            float acceltransparancy = ((Mathf.Abs((float)accelx + (float)accelz + (float)accely))*1.33f);

            panelx.GetComponent<Image>().color = new Color((float)colcodey, (float)colcodex, (float)colcodez, (float)acceltransparancy + (float)0.25);

            descrptor2.GetComponent<Text>().text = "tilt x:" + colcodex.ToString();
            descrptor3.GetComponent<Text>().text = "tilt y:" + colcodey.ToString();
            descrptor1.GetComponent<Text>().text = "tilt z:" + colcodez.ToString();
            



        }
           
    }
   
    
   

    void Update()
    {
        if (pressed)
        {
            if (SystemInfo.supportsGyroscope)
            {
                Descriptor();
                Debug.Log("The accel is:");

                

            }
        }

    }

}