using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class PlayerCon : MonoBehaviourPunCallbacks
{
    public GameObject userInfo;
    public Transform userPanel;
    public GameObject startButton;
    public GameObject readyButton;
    public Text playerCountText;
    public GameObject UserInfoPanel;

    public bool isReady = false;

    private void Start()
    {
        StartCoroutine(CreatePlayer());
        if (PhotonNetwork.IsMasterClient)
        {
            //들어오면 플레이어 생성
            //startButton.SetActive(true);
            startButton.GetComponent<Button>().onClick.AddListener(StartGame);
        }
        else
        {
            readyButton.SetActive(true);
            readyButton.GetComponent<Button>().onClick.AddListener(ReadyGame);
        }
        GetConnectPlayerCount();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.ToStringFull());
        GetConnectPlayerCount();
        CheckAllPlayersReady();
    }

    void GetConnectPlayerCount()
    {
        Room currRoom = PhotonNetwork.CurrentRoom;
        playerCountText.text = currRoom.PlayerCount.ToString() + " / " + currRoom.MaxPlayers.ToString();
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
        isReady = !isReady;
        Debug.Log("ButtonClick" +  isReady);
        PhotonView[] pvs = FindObjectsOfType<PhotonView>();
        
        foreach(PhotonView pv in pvs)
        {
            if(pv.IsMine)
            {
               foreach(UserInfo info in FindObjectsOfType<UserInfo>())
                {
                    if(pv.GetComponent<CharacterCustom>().name==info.nickname)
                    {
                        info.isReady = isReady;
                        info.DisplayPlayerInfo(); 
                        readyButton.GetComponent<PhotonView>().RPC("SetPlayerReady", RpcTarget.OthersBuffered, pv.GetComponent<CharacterCustom>().name, isReady);
                    }
                }
            }
        }
      
    }

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
            startButton.SetActive(true);
            Debug.Log("모든 플레이어가 준비함");
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
        Debug.Log(FindObjectsOfType<PhotonView>().Length);
    }

    IEnumerator CreatePlayer()
    {
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

        }
    }
    public void CreatePanel(PhotonView view)
    {
        GameObject userInfoPanel = Instantiate(UserInfoPanel, Vector3.zero, Quaternion.identity);

        SaveLoad saveLoadScript = FindObjectOfType<SaveLoad>();
        string nickname = saveLoadScript.nickName;

        UserInfo ui = userInfoPanel.GetComponent<UserInfo>();
        ui.nickname = nickname;
        ui.DisplayPlayerInfo();
        view.RPC("UpdateNickname", RpcTarget.OthersBuffered, ui.nickname);


    }

    [PunRPC]
    void UpdateNickname(string playerName)
    {
        GameObject userInfoPanel = Instantiate(UserInfoPanel, Vector3.zero, Quaternion.identity);
        UserInfo ui = userInfoPanel.GetComponent<UserInfo>();
        ui.nickname = playerName;
        ui.DisplayPlayerInfo();
    }
}
