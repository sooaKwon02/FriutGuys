using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviourPunCallbacks
{
    //[HideInInspector]
    public Text userName;
    public Image readyGame;
    public GameObject kickGame;

    [HideInInspector]
    public bool isReady;
    //private SaveLoad saveLoadScript;
    private Transform userPanel;

    private void Awake()
    {
        isReady = false;
        readyGame.enabled = false;
        userPanel = GameObject.FindGameObjectWithTag("UserInfoPanel").transform;
        transform.SetParent(userPanel.transform);
        
    }
    public void DisplayPlayerInfo(string _nick)
    {
        userName.text = _nick;
        if (PhotonNetwork.IsMasterClient)
        {
            kickGame.SetActive(true);
        }
        else
        {
            kickGame.SetActive(false);
        }
    } 
    public void Ready()
    {
        if (!isReady)
        {
            isReady = true;
            readyGame.enabled=true;
        }
        else
        {
            isReady = false;
            readyGame.enabled=false;
        }
    }   
}
