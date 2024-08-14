using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XRController : MonoBehaviour
{
    //public GameObject XRI;
    //public GameObject XRO;
    //public GameObject cam;



    public GameObject vrCheck;
    public int count;
   
    private void Start()
    {
        count = 0;
        StartCoroutine(Check());
    }
    
    IEnumerator Check()
    {
        if (count < 5)
        {
            count++;
            if (!XRSettings.isDeviceActive)
            {
                vrCheck.SetActive(true);
                yield return new WaitForSeconds(1.0f);
                StartCoroutine(Check());
            }
        }
        else
        {
            vrCheck.SetActive(false);
            SceneReturn();
        }       
    }

    public void SceneReturn()
    {
        SceneManager.LoadScene(2);
    }
}
