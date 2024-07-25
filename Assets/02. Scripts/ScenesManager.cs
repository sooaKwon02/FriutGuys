using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private static ScenesManager _instance;
    public static ScenesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScenesManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("ScenesManager");
                    _instance = singletonObject.AddComponent<ScenesManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    static public int SceneNum;
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
        SceneNum = 1;
        SceneManager.LoadScene(SceneNum);
    }

}
