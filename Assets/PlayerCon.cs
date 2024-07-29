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
    public Button startButton;
    public Text readyText;
    bool ready;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CreatePlayer());
            readyText.text = "시작";
        }           
    }
   
    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(4);
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
            readyText.text = "준비";
        }
    }
    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    Debug.Log("asd");
    //    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    //    foreach (GameObject player in players) 
    //    {
    //        PhotonView pv=GetComponent<PhotonView>();
    //        if(pv.IsMine)
    //            player.GetComponentInChildren<Camera>().enabled = true;
    //        else
    //            player.GetComponentInChildren<Camera>().enabled = false;
    //    }
      
    //}
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
