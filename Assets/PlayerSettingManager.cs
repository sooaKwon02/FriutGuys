using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSettingManager : MonoBehaviourPunCallbacks
{
    public static PlayerSettingManager Instance { get; private set; }
    string PlayerName;
    GameObject gamePlayer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
        if (FindObjectOfType<DatabaseManager>())
        { 
            PlayerName = FindObjectOfType<DatabaseManager>().nickname.text.ToString();
        }
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        base.OnConnectedToMaster();
        Debug.Log("�κ�����");
    }
    public override void OnJoinedRoom()
    {
        StartCoroutine(LoadSceneAsync(3));
    }
    public override void OnLeftRoom()
    {
        StartCoroutine(LoadSceneAsync(2));
    }
    private IEnumerator LoadSceneAsync(int SceneNum)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNum);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            yield return null;
        }

        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            PhotonView[] photons = FindObjectsOfType<PhotonView>();
            gamePlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            GameObject userInfoPanel = GameObject.FindGameObjectWithTag("UserInfoPanel");
            if (userInfoPanel != null)
            {
                GameObject userinfo = PhotonNetwork.Instantiate("UserInfo", Vector3.zero, Quaternion.identity);
                userinfo.GetComponent<UserInfo>().SetUserInfo(PlayerName);
                userinfo.transform.SetParent(userInfoPanel.transform, false);

            }
        }
    }
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        foreach (FriendInfo friend in friendList)
        {
            if (friend.IsInRoom)
            {
                Debug.Log($"ģ���� �濡 ����: {friend.UserId}");
            }
        }
    }














    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� �����߽��ϴ�.");
    }

    public override void OnLeftLobby()
    {
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("����:"+ newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("����:"+ otherPlayer.NickName);
    }
    public void KickPlayerByNickname(string playerNickname)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == playerNickname)
            {
                PhotonNetwork.CloseConnection(player);
                return; 
            }
        }
    }
}
