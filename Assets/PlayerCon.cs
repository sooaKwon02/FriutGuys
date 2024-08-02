using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviourPunCallbacks
{
    public static PlayerCon instance;

    private string playerName;
    private SaveLoad saveLoadScript;

    public GameObject userInfo;
    public Transform userPanel;
    public Button startButton;
    public Text readyText;

    private bool isReady = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //들어오면 플레이어 생성
            StartCoroutine(CreatePlayer());
            startButton.onClick.AddListener(StartGame);
            readyText.text = "시작";
        }
        else
        {
            StartCoroutine(CreatePlayer());
            startButton.onClick.AddListener(ReadyGame);
            readyText.text = "준비";
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

        UserInfo userinfo = FindObjectOfType<UserInfo>();
        if(userinfo != null)
        {
            userinfo.SetReady();
        }
    }

    public void CheckAllPlayersReady()
    {
        UserInfo[] userInfos = FindObjectsOfType<UserInfo>();
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            bool playerReady = false;
            foreach(UserInfo userif in userInfos)
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
                    startButton.enabled = false;
                }
                return;
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.enabled = true;
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
          
            
            if (GameObject.FindGameObjectWithTag("UserInfoPanel"))
            {
                GameObject obj = PhotonNetwork.Instantiate("UserInfoPanel", Vector3.zero, Quaternion.identity);
            }
        }
    }
}
