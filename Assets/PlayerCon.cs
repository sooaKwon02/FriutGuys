using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviourPunCallbacks
{
    private string playerName;
    public GameObject userInfo;
    public Text readyText;
    bool ready;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CreatePlayer());
        }
        if (PhotonNetwork.IsMasterClient)
        {
            readyText.text = "시작".ToString();
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            readyText.text = "준비".ToString();
        }
    }
   
    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }
   
    public void ReadyGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            if (FindObjectOfType<UserInfo>())
            {
                UserInfo[] _infos = FindObjectsOfType<UserInfo>();
                foreach (UserInfo _info in _infos)
                {
                    if(_info.userName.text== playerName)
                    {
                        if(_info.readyCheck==false)
                            _info.Ready(true);
                        else
                            _info.Ready(false); 
                    }
                }
            }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            if (FindObjectOfType<UserInfo>())
            {
                UserInfo[] _infos = FindObjectsOfType<UserInfo>();
                foreach (UserInfo _info in _infos)
                {
                    if (_info.readyCheck == true)
                    {
                        _info.Ready(true);
                    }
                }
            }
        }

    }
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CreatePlayer());
        }
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
