using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    string myName = "UserName";
    GameObject gamePlayer;
    public InputField roomNameInput;
    public InputField fullRoomInput;
    public InputField passwordInput;
    public bool secretCheck;
    public Toggle toggle;
    public Transform roomListPanel;
    public GameObject roomListButtonPrefabs;
    public GameObject createRoomFailPanel;
    public string version = "Ver 0.1.0";

    void Awake()
    {
        if (toggle != null)
        {
            secretCheck = false;
            toggle.isOn = false;
        }
        if (passwordInput != null)
        {
            passwordInput.enabled = !secretCheck;
            createRoomFailPanel.SetActive(false);
        }
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AddCallbackTarget(this);
        }
    }

    public void SecretCheckButton()
    {
        secretCheck = !secretCheck;
        passwordInput.text = string.Empty;
        passwordInput.enabled = !secretCheck;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        base.OnConnectedToMaster();
        Debug.Log("�κ�����");
    }

    public void OnClickCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true
        };
        byte maxPlayers;
        if (byte.TryParse(fullRoomInput.text, out maxPlayers) && maxPlayers > 1 && maxPlayers < 17)
        {
            roomOptions.MaxPlayers = maxPlayers;
            PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);
        }
        else
        {
            StartCoroutine(CreateRoomFail());
        }
    }

    IEnumerator CreateRoomFail()
    {
        createRoomFailPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        createRoomFailPanel.SetActive(false);
    }

    public override void OnCreatedRoom()
    {
        // �� ���� �� ó���� ���� �߰�
    }

    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(LoadSceneAsync(3)); 
    }

    public override void OnLeftRoom()
    {
        StartCoroutine(LoadSceneAsync(2));
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"�� ���� ����: {message}");
    }

    private IEnumerator LoadSceneAsync(int SceneNum)
    {
        Debug.Log("Starting scene load: " + SceneNum);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNum);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading Progress: " + (progress * 100) + "%");
            yield return null;
        }

        Debug.Log("Scene Loaded");

        // �� �ε� �� �÷��̾� ����
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            if (gamePlayer == null)
            {
                Debug.Log(PhotonNetwork.PlayerList);
                Debug.Log("asd");
                gamePlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

                // UserInfoPanel�� UserInfo ����
                GameObject userInfoPanel = GameObject.FindGameObjectWithTag("UserInfoPanel");
                if (userInfoPanel != null)
                {
                    GameObject userinfo = PhotonNetwork.Instantiate("UserInfo", Vector3.zero, Quaternion.identity);
                    userinfo.GetComponent<UserInfo>().SetUserInfo(myName);
                    userinfo.transform.SetParent(userInfoPanel.transform, false);
                }
            }
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomListPanel != null)
        {
            foreach (Transform child in roomListPanel)
            {
                Destroy(child.gameObject);
            }
        }
        else
            return;

        // �� ����� ������Ʈ
        foreach (RoomInfo _room in roomList)
        {
            GameObject list = Instantiate(roomListButtonPrefabs);
            if (list != null)
            {
                RoomList roomListComponent = list.GetComponent<RoomList>();
                if (roomListComponent != null)
                {
                    roomListComponent.CreateRoomInfo(_room.Name, _room.PlayerCount, _room.MaxPlayers, secretCheck, passwordInput.text);
                    list.transform.SetParent(roomListPanel, false);
                }
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� �����߽��ϴ�.");
    }

    public override void OnLeftLobby()
    {
        // �κ� ���� ���� �� ó���� ���� �߰�
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

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        // �κ� ��� ������Ʈ �� ó���� ���� �߰�
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} has entered the room.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // �÷��̾ ���� ������ �� UserInfo ������Ʈ ����
        UserInfo[] users = FindObjectsOfType<UserInfo>();
        foreach (UserInfo userinfo in users)
        {
            if (userinfo.userName.ToString() == otherPlayer.NickName) // ����� �̸����� ��
            {
                Destroy(userinfo.gameObject);
            }
        }
    }

    public void KickPlayerByNickname(string playerNickname)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == playerNickname)
            {
                PhotonNetwork.CloseConnection(player);
                return; // ù ��°�� ã�� �÷��̾ ������ �� �Լ� ����
            }
        }
    }
}
