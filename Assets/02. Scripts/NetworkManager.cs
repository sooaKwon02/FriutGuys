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
        Debug.Log("로비접속");
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
        // 방 생성 시 처리할 내용 추가
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
        Debug.LogError($"방 생성 실패: {message}");
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

        // 씬 로딩 후 플레이어 생성
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            if (gamePlayer == null)
            {
                Debug.Log(PhotonNetwork.PlayerList);
                Debug.Log("asd");
                gamePlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

                // UserInfoPanel과 UserInfo 설정
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

        // 방 목록을 업데이트
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
        Debug.Log("로비에 입장했습니다.");
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
        // 플레이어가 방을 떠났을 때 UserInfo 오브젝트 삭제
        UserInfo[] users = FindObjectsOfType<UserInfo>();
        foreach (UserInfo userinfo in users)
        {
            if (userinfo.userName.ToString() == otherPlayer.NickName) // 사용자 이름으로 비교
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
                return; // 첫 번째로 찾은 플레이어를 강퇴한 후 함수 종료
            }
        }
    }
}
