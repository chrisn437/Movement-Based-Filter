using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Camera Scene");
    }
}
