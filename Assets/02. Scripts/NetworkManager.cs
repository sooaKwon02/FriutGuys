using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
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
        DontDestroyOnLoad(this);
        PhotonNetwork.ConnectUsingSettings();
        secretCheck = false;
        toggle.isOn = false;
        if (passwordInput != null)
            passwordInput.enabled = !secretCheck;
        createRoomFailPanel.SetActive(false);
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
        // 방 생성 성공 후 처리할 내용 추가
    }

    public override void OnJoinedRoom()
    {
        // 방 입장 성공 후 처리할 내용 추가
    }

    IEnumerator LoadStage()
    {
        PhotonNetwork.IsMessageQueueRunning = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync(3);
        yield return ao;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 기존 UI 업데이트를 먼저 지우기
        foreach (Transform child in roomListPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomInfo _room in roomList)
        {
            GameObject list = Instantiate(roomListButtonPrefabs);
            list.GetComponent<RoomList>().CreateRoomInfo(_room.Name, _room.PlayerCount, _room.MaxPlayers, secretCheck, passwordInput.text);
            list.transform.SetParent(roomListPanel, false);
        }
    }

    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        // 로비 입장 성공 후 처리할 내용 추가
    }

    public override void OnLeftLobby()
    {
        // 로비 퇴장 성공 후 처리할 내용 추가
    }

    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        foreach (FriendInfo friend in friendList)
        {
            if (friend.IsInRoom)
            {
                Debug.Log($"친구가 방에 있음: {friend.UserId}");
            }
        }
    }

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        // 로비 통계 업데이트 후 처리할 내용 추가
    }
}
