using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public Text userName;
    public Image readyGame;
    public GameObject kickGame;
    [HideInInspector]
    public bool isReady;

    public string nickname;
    private SaveLoad saveLoadScript;
    public Transform userPanel;

    private void Start()
    {
        //왜 안될까....
        this.transform.SetParent(userPanel.transform);
    }
    public void DisplayPlayerInfo()
    {
        saveLoadScript = FindObjectOfType<SaveLoad>();
        nickname = saveLoadScript.nickName;
        userName.text = nickname;

        if (PhotonNetwork.IsMasterClient)
        {
            if (!photonView.IsMine)
            {
                kickGame.SetActive(true);
            }
            else
            {
                kickGame.SetActive(false);
            }
        }
        else
        {
            isReady = false;
            kickGame.SetActive(false);
        }

    }

    public void SetReady()
    {
        isReady = true;
        photonView.RPC("UpdateReadyStatus", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, isReady);
    }

    [PunRPC]
    void UpdateReadyStatus(string playerName, bool ready)
    {
        if(userName.text == playerName)
        {
            isReady = ready;
        }
        PlayerCon.instance.CheckAllPlayersReady();
    }

    //private void Awake()
    //{
    //    readyCheck = false;
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        if(!photonView.IsMine)
    //        {
    //            kickGame.SetActive(true);
    //        }
    //        else
    //        {
    //            kickGame.SetActive(false);
    //        }
    //    }              
    //    else
    //    {
    //        kickGame.SetActive(false);
    //    }
    //    if (GameObject.FindGameObjectWithTag("UserInfoPanel"))
    //    {
    //        transform.SetParent(GameObject.FindGameObjectWithTag("UserInfoPanel").transform);   
    //    }

    //}
    //public void KickOutButton()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        PhotonView pv = gameObject.GetComponent<PhotonView>();
    //        photonView.RPC("TriggerPlayerKick",pv.Owner);
    //    }
    //}
    //[PunRPC]
    //void TriggerPlayerKick()
    //{
    //    PhotonNetwork.LeaveRoom();
    //}
    //public void Ready(bool check)
    //{
    //    if (check)
    //    {
    //        readyGame.color = new Color(0, 1, 0, 1);
    //        readyCheck = true;
    //    }
    //    else
    //    {
    //        readyGame.color = new Color(1, 1, 1, 0.5f);
    //        readyCheck = false;
    //    }
    //}
    //public void SetUserInfo(string _name)
    //{
    //    userName.text = _name;
    //    PhotonNetwork.NickName = _name;
    //}
}
