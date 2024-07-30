using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroy : MonoBehaviour
{
  
    private static DonDestroy _instance;

    public static DonDestroy Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DonDestroy>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DonDestroy).Name);
                    _instance = singletonObject.AddComponent<DonDestroy>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
    void OnApplicationQuit()
    {
        
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

