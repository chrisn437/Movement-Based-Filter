using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamAccess : MonoBehaviour
{

    int currentCamIndex = 0;

    private bool camAvailable;
    private WebCamTexture backCam;
    public RawImage display;

    public Texture defaultBackground;
    public AspectRatioFitter fit;
    

    public void SwapCam_Clicked()
    {
        if(WebCamTexture.devices.Length > 0)
        {
            currentCamIndex += 1;
            currentCamIndex %= WebCamTexture.devices.Length;

            if(backCam != null)
            {
                StopWecam();
                Start();
            }

        }
    }

    private void StopWecam()
    {
        display.texture = null;
        backCam.Stop();
        backCam = null;
    }



    private void Start()
    {

        defaultBackground = display.texture;

        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            camAvailable = false;
            return;
        }

       
        backCam = new WebCamTexture(devices[currentCamIndex].name, Screen.width, Screen.height);
            

        if(backCam == null)
        {
            return;
        }


        display.material.mainTexture = backCam;

        backCam.Play();

        display.texture = backCam;
        camAvailable = true;

    }

    private void Update()
    {
        if (!camAvailable)
            return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f: 1f;
        display.rectTransform.localScale = new Vector3(1f * ratio, scaleY * ratio, 1f * ratio);

        int orient = -backCam.videoRotationAngle;
        display.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

}
