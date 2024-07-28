using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviourPunCallbacks
{
    private string playerName;
    public GameObject userInfo;
    bool ready;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CreatePlayer());
        }
    }
   
    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }
   
    public void ReadyGame()
    {
        ready = !ready;
        UserInfo[] infos= FindObjectsOfType<UserInfo>();
        foreach(UserInfo info in infos)
        {
            if(info.userName.text== playerName)
            {
                info.Ready(true);
            }
        }
    }
    public override void OnJoinedRoom()
    {
        StartCoroutine(CreatePlayer());
    }
    IEnumerator CreatePlayer()
    {
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            if (GameObject.FindGameObjectWithTag("UserInfoPanel"))
            {
                GameObject obj = PhotonNetwork.Instantiate("UserInfo", Vector3.zero, Quaternion.identity);
            }
        }
    }
}
