using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRController : MonoBehaviour
{
    public void VRGameStart()
    {
        SceneManager.LoadScene("VR Map");
    }

    public void VRRoomExit()
    {
        SceneManager.LoadScene("1.MainScene");
    }
}