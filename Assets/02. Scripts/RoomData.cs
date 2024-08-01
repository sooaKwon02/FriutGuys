using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    [HideInInspector]
    public string roomName = "";

    [HideInInspector]
    public int connectPlayer = 0;

    [HideInInspector]
    public int maxPlayers = 0;

    [HideInInspector]
    public string password = "";

    public Text textRoomName;
    public Text textConnectInfo;

    public void DisplayRoomData()
    {
        textRoomName.text = roomName;
        textConnectInfo.text = "(" + connectPlayer.ToString() + " / " +  maxPlayers.ToString() + password +  ")";
        Debug.Log(maxPlayers);
    }
}
