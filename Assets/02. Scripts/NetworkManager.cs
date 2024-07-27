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
       
    }
    public void SecretCheckButton()
    {
        secretCheck = !secretCheck;
        passwordInput.text = string.Empty;
        passwordInput.enabled = !secretCheck;
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
    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
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
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        // 로비 통계 업데이트 후 처리할 내용 추가
    }    
}
