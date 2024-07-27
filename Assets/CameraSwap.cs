using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    Camera mainCamera;
    Camera characterCamera;

    private void Awake()
    {
        characterCamera = GetComponent<Camera>();
        if(FindObjectsOfType<Camera>().Length>1)
        {
           Camera[] Cameras = FindObjectsOfType<Camera>();
            foreach(Camera _mainCamera in Cameras)
            {
                if (_mainCamera.tag == "MainCamera")
                {
                    mainCamera = _mainCamera;
                }
                else
                    mainCamera = null;
            }
        }
        if(mainCamera==null)
        {
            characterCamera.enabled = true;
        }
        else
        {
            characterCamera.enabled = false;
        }
    }

    void Update()
    {
        
    }
}
