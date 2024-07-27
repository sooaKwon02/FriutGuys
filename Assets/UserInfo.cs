using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviourPun
{
    [HideInInspector]
    public Text userName;
    public GameObject readyGame;
    public GameObject leaveGame;
    public GameObject kickGame;
    bool readyCheck;

    private void Awake()
    {
        readyCheck = false;
        if (PhotonNetwork.IsMasterClient)
        {
            leaveGame.SetActive(true);
            kickGame.SetActive(false);
        }
        else
        {
            leaveGame.SetActive(false);
            kickGame.SetActive(true);
        }
    }
    public void KickOutButton()
    {
        FindObjectOfType<PlayerSettingManager>().KickPlayerByNickname(userName.text.ToString());
    }
    public void LeaveButton()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    public void ReadyButton()
    {
        readyCheck=!readyCheck;
        if (readyCheck)
        {
            readyGame.GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            readyGame.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }
    public void SetUserInfo(string _name)
    {
        if(photonView.IsMine)
        {
            userName.text = _name;
            PhotonNetwork.NickName = _name;
        }
    }
}
