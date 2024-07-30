using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    static public int SceneNum;
    private void Awake()
    {
        SceneNum = 1;
        SceneManager.LoadScene(SceneNum);
    }      
   

}
