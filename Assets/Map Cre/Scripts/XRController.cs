using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class XRController : MonoBehaviour
{
    public GameObject mainCamera;
    private GameObject XRI;
    private GameObject XRO;
    private GameObject XRC;

    private void Awake()
    {
        XRI = GameObject.FindGameObjectWithTag("XR I");
        XRO = GameObject.FindGameObjectWithTag("XR O");
        XRC = GameObject.FindGameObjectWithTag("XR C");
    }

    private void Start()
    {
        if (XRSettings.isDeviceActive)
        {
            mainCamera.SetActive(false);
            XRI.SetActive(true);
            XRO.SetActive(true);
            XRC.SetActive(true);
        }

        else
        {
            mainCamera.SetActive(true);
            XRI.SetActive(false);
            XRO.SetActive(false);
            XRC.SetActive(false);
        }
    }

    private void Update()
    {
        if (XRSettings.isDeviceActive)
        {
            mainCamera.SetActive(false);
            XRI.SetActive(true);
            XRO.SetActive(true);
            XRC.SetActive(true);
        }

        else
        {
            mainCamera.SetActive(true);
            XRI.SetActive(false);
            XRO.SetActive(false);
            XRC.SetActive(false);
        }
    }

    public void VRRoomEnter()
    {
        SceneManager.LoadScene("VR Room");
    }
}
