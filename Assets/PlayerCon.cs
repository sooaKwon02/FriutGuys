using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class PlayerCon : MonoBehaviourPunCallbacks
{
    public Text userName;
    public GameObject userInfo;
    public Transform userPanel;
    public GameObject startButton;
    public GameObject readyButton;

    public bool isReady = false;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //들어오면 플레이어 생성
            StartCoroutine(CreatePlayer());
            startButton.SetActive(true);
            startButton.GetComponent<Button>().onClick.AddListener(StartGame);
        }
        else
        {
            StartCoroutine(CreatePlayer());
            readyButton.SetActive(true);
            readyButton.GetComponent<Button>().onClick.AddListener(ReadyGame);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CheckAllPlayersReady();
    }

    public void LeaveGame()
    {
        //나가기
        PhotonNetwork.LeaveRoom();
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(4); //랜덤으로 받아오자
        }
    }

    public void ReadyGame()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    if (FindObjectOfType<UserInfo>())
        //    {
        //        UserInfo[] _infos = FindObjectsOfType<UserInfo>();
        //        foreach (UserInfo _info in _infos)
        //        {
        //            if (_info.userName.text == playerName)
        //            {
        //                isReady = !_info.isReady;
        //                break;
        //            }
        //        }
        //    }
        //}
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    if (FindObjectOfType<UserInfo>())
        //    {
        //        UserInfo[] _infos = FindObjectsOfType<UserInfo>();
        //        foreach (UserInfo _info in _infos)
        //        {
        //            if (_info.readyCheck == true)
        //            {
        //                _info.Ready(true);
        //            }
        //        }
        //    }
        //}

        //UserInfo userinfo = FindObjectOfType<UserInfo>();
        //if (userinfo != null)
        //{
        //    userinfo.SetReady();
        //    photonView.RPC("CheckAllPlayersReady", RpcTarget.MasterClient);
        //}
        isReady = !isReady;
        photonView.RPC("SetPlayerReady", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, isReady);
    }

    [PunRPC]
    public void SetPlayerReady(string playerName, bool ready)
    {
        UserInfo[] userInfos = FindObjectsOfType<UserInfo>();
        foreach(UserInfo userInfo in userInfos)
        {
            if(userInfo.userName.text == playerName)
            {
                userInfo.isReady = ready;
                userInfo.DisplayPlayerInfo();
                break;
            }
        }
        CheckAllPlayersReady();
    }

    [PunRPC]
    public void CheckAllPlayersReady()
    {
        UserInfo[] userInfos = FindObjectsOfType<UserInfo>();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            bool playerReady = false;
            foreach (UserInfo userif in userInfos)
            {
                if (userif.userName.text == player.NickName && userif.isReady)
                {
                    playerReady = true;
                    break;
                }
            }

            if (!playerReady)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    startButton.GetComponent<Button>().interactable = false;
                }
                return;
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.GetComponent<Button>().interactable = true;
            Debug.Log("모든 플레이어가 준비함");
        }
    }
    //public override void OnJoinedRoom()
    //{
    //    if (!PhotonNetwork.IsMasterClient)
    //    {
    //        StartCoroutine(CreatePlayer());
    //        readyText.text = "준비";
    //    }
    //}
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

    //[PunRPC]
    //void UpdateReady(Player newPlayer, bool ready)
    //{
    //    if (FindObjectOfType<UserInfo>())
    //    {
    //        UserInfo[] _info = FindObjectsOfType<UserInfo>();
    //        foreach (UserInfo _if in _info)
    //        {
    //            if (_if.userName.text == newPlayer.NickName)
    //            {
    //                _if.isReady = ready;
    //                _if.DisplayPlayerInfo();
    //                break;
    //            }
    //        }
    //    }
    //    CheckAllPlayersReady();
    //}

    IEnumerator CreatePlayer()
    {
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

            GameObject userInfoPanel = PhotonNetwork.Instantiate("UserInfoPanel", Vector3.zero, Quaternion.identity);

            SaveLoad saveLoadScript = FindObjectOfType<SaveLoad>();
            string nickname = saveLoadScript.nickName;

            PhotonView photonView = userInfoPanel.GetComponent<PhotonView>();
            photonView.RPC("UpdateNickname", RpcTarget.AllBuffered, nickname);
        }
    }
}
