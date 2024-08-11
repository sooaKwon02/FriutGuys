using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static SaveLoad;
using System.Collections.Generic;

public class PlayerCon : MonoBehaviourPunCallbacks
{
    public string nickName;
    public GameObject userInfo;
    public Transform userPanel;

    public Button kickButton;
    public GameObject startButton;
    public GameObject readyButton;
    public Text playerCountText;
    public GameObject UserInfoPanel;

    public List<int> loadScenes = new List<int>();
    public int[] sceneIndex = new int[] {5, 6, 7, 8, 9};

    private void Awake()
    {
        nickName = FindObjectOfType<SaveLoad>().nickName;
    }
    private void Start()
    {
        StartCoroutine(CreatePlayer());
        startButton.SetActive(true);
        if (PhotonNetwork.IsMasterClient) 
        { 
            startButton.GetComponent<Button>().onClick.AddListener(CheckAllPlayersReady);
        }
        else 
        {
            readyButton.GetComponent<Button>().onClick.AddListener(ReadyGame); 
        }
        GetConnectPlayerCount();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(" name : " + newPlayer.NickName);
        GetConnectPlayerCount();
    }

    void GetConnectPlayerCount()
    {
        Room currRoom = PhotonNetwork.CurrentRoom;
        playerCountText.text = currRoom.PlayerCount.ToString() + " / " + currRoom.MaxPlayers.ToString();
    }

    public void LeaveGame()
    {
        RemoveUserInfoPanel();
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GetConnectPlayerCount();
    }

    public void ReadyGame()
    {
        PhotonView[] pvs = FindObjectsOfType<PhotonView>();
        foreach (PhotonView _pv in pvs)
        {
            if (_pv.IsMine&&_pv.GetComponent<CharacterCustom>())
            {
                _pv.RPC("SetPlayerReady", RpcTarget.AllBuffered, nickName);
            }
        }
    }

    public void CheckAllPlayersReady()
    {
        int count = 0;
        UserInfo[] userInfos = FindObjectsOfType<UserInfo>();
        foreach (UserInfo userif in userInfos)
        {
            if (userif.isReady)
            {
                count++;
            }
        }
        if (PhotonNetwork.IsMasterClient && count == PhotonNetwork.CurrentRoom.MaxPlayers - 1)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            //int num = Random.Range(4, 6);
            //if(num>=4&&num<6)
            //{
            //    PlayerSettingManager.Instance.type=PlayerSettingManager.GAME_TYPE.RACING;
            //}
            //else if(num>=6&&num<7)
            //{
            //    PlayerSettingManager.Instance.type = PlayerSettingManager.GAME_TYPE.BATTLE;
            //}
            //PhotonNetwork.LoadLevel(5);
            LoadRandomScene();
        }
    }

    public void LoadRandomScene()
    {
        List<int> sceneList = new List<int>(sceneIndex);

        foreach(int index in loadScenes)
        {
            sceneList.Remove(index);
        }

        if(sceneList.Count == 0)
        {
            loadScenes.Clear();
            sceneList = new List<int>(sceneIndex);
        }

        int randomIndex = Random.Range(5, 10);
        int selectScene = sceneList[randomIndex];

        loadScenes.Add(selectScene);

        PhotonNetwork.LoadLevel(selectScene);
    }

    IEnumerator CreatePlayer()
    {
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            float xDir = Random.Range(-10f, 10f);
            float zDir = Random.Range(-10f, 10f);
            GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(xDir, 0, zDir), Quaternion.identity);
        }
    }

    public void UserInfoSet(string _name)
    {
        GameObject userInfoPanel = Instantiate(UserInfoPanel);
        userInfoPanel.GetComponent<UserInfo>().DisplayPlayerInfo(_name);
        userPanel.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        userPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(userPanel.GetComponent<GridLayoutGroup>().cellSize.x, userPanel.GetComponent<GridLayoutGroup>().cellSize.y * userPanel.childCount);

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            readyButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            readyButton.SetActive(true);
        }
    }

    public void RemoveUserInfoPanel()
    {
        PhotonView[] pvs = FindObjectsOfType<PhotonView>();
        foreach (PhotonView _pv in pvs)
        {
            if (_pv.IsMine&&_pv.GetComponent<PlayerCtrl>())
            {
                _pv.RPC("DestroyUserInfo", RpcTarget.All, nickName);
            }
        }
    }

    public void KickPlayer(string name)
    {
        //������ Ŭ���̾�Ʈ�϶�
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView[] pvs = FindObjectsOfType<PhotonView>();
            foreach (PhotonView _pv in pvs)
            {
                //����信�� RPC�� ���
                _pv.RPC("KickPlayerRPC", RpcTarget.All, name);
                _pv.RPC("DestroyUserInfo", RpcTarget.All, name);
            }
        }
    }
}
