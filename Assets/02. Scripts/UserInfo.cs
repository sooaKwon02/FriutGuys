using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviourPunCallbacks
{
    public Text userName;
    public Image readyGame;
    public GameObject kickGame;

    [HideInInspector]
    public bool isReady;
    [HideInInspector]
    public bool isKick;

    private Transform userPanel;

    private void Awake()
    {
        isReady = false;
        readyGame.enabled = false;
        kickGame.GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerCon playerCon = FindObjectOfType<PlayerCon>();
            playerCon.KickPlayer(userName.text);
        });
        userPanel = GameObject.FindGameObjectWithTag("UserInfoPanel").transform;
        transform.SetParent(userPanel.transform,false);
    }

    public void DisplayPlayerInfo(string _nick)
    {
        userName.text = _nick;
        if (PhotonNetwork.IsMasterClient)
        {
            kickGame.SetActive(true);
            if (PhotonNetwork.MasterClient.NickName == _nick)
            {
                kickGame.SetActive(false);
            }
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
            readyGame.enabled = true;
        }
        else
        {
            isReady = false;
            readyGame.enabled = false;
        }
    }
}
