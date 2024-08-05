using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviourPunCallbacks
{
    public Text userName;
    public GameObject readyGame;
    public GameObject kickGame;

    [HideInInspector]
    public bool isReady;

    public string nickname;
    //private SaveLoad saveLoadScript;
    private Transform userPanel;

    private void Awake()
    {
        userPanel = GameObject.FindGameObjectWithTag("UserInfoPanel").transform;
        transform.SetParent(userPanel.transform);
    }
    public void DisplayPlayerInfo()
    {
        userName.text = nickname;
        readyGame.SetActive(isReady);

        if (PhotonNetwork.IsMasterClient)
        {
            //userName.color = Color.blue;
           
                kickGame.SetActive(true);            
           
                //
                //kickGame.SetActive(false);
            
        }
        else
        {
            kickGame.SetActive(false);
        }

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
