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
    public InputField inputPw;
    public bool secretCheck;
    public Toggle toggle;
    public Transform roomListPanel;
    public GameObject roomListButtonPrefabs;
    public GameObject createRoomFailPanel;

    private DatabaseManager csDbManager;
    public GameObject scrollContents;
    public GameObject pwPanel;

    
    private string selectedRoomName;

    private string roomPassword = "";
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

        roomListButtonPrefabs.GetComponent<RectTransform>().pivot = new Vector3(0.0f, 1.0f);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joinde Lobby");
    }
    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("User_ID");

        return userId;
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        base.OnConnectedToMaster();
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
            if(!secretCheck && !string.IsNullOrEmpty(passwordInput.text))
            {
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "Password", passwordInput.text } };
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "Password" };
            }
            bool isSucces = PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);

            Debug.Log("�� ���� �Ϸ� : " + isSucces);
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


    public override void OnJoinedRoom()
    {
        Debug.Log("Room ����");

        //if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("Password", out object password))
        //{
        //     roomPassword = password as string;
        //    if (!string.IsNullOrEmpty(roomPassword))
        //    {
        //        pwPanel.gameObject.SetActive(true);
        //        inputPw.onEndEdit.AddListener(OnPasswordEntered);
        //    }
        //    else
        //    {
        //        pwPanel.gameObject.SetActive(false);
        //        Debug.Log("��й�ȣ ����");
        //    }
        //}

    }
    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.JoinRoom(roomNameInput.text);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���� �� ���� ����, �� ���� " + message);
        StartCoroutine(JoinRoomFail());
    }

    IEnumerator JoinRoomFail()
    {
        createRoomFailPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        createRoomFailPanel.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("�÷��̾� ���� : " + newPlayer.NickName);
        CheckRoomPlayerCount();
    }

    private void CheckRoomPlayerCount()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("���� ��");
            PhotonNetwork.LeaveRoom();
            StartCoroutine(RoomFull());
        }
    }

    IEnumerator RoomFull()
    {
        createRoomFailPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        createRoomFailPanel.SetActive(false);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }
        int rowCount = 0;
        scrollContents.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

       foreach(RoomInfo _room in roomList)
       {
            Debug.Log(_room.Name);
            GameObject room = (GameObject)Instantiate(roomListButtonPrefabs);
            room.transform.SetParent(scrollContents.transform, false);

            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.Name;
            roomData.connectPlayer = _room.PlayerCount;
            roomData.maxPlayers = _room.MaxPlayers;

            roomData.DisplayRoomData();

            roomData.GetComponent<Button>().onClick.AddListener(delegate { OnClickRoomItem(roomData.roomName); Debug.Log("Room Click " + roomData.roomName); });

            scrollContents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20f);
       }
       
    }

    void OnClickRoomItem(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);

        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("Password", out object password))
        {
            roomPassword = password as string;
            if (!string.IsNullOrEmpty(roomPassword))
            {
                pwPanel.gameObject.SetActive(true);
                inputPw.onEndEdit.AddListener(OnPasswordEntered);
            }
            else
            {
                pwPanel.gameObject.SetActive(false);
                Debug.Log("��й�ȣ ����");
            }
        }
    }

    private void OnPasswordEntered(string enteredPassword)
    {
        if (enteredPassword == roomPassword)
        {
            Debug.Log("��й�ȣ ��ġ");
            pwPanel.gameObject.SetActive(false);
            // Proceed with room functionality
        }
        else
        {
            Debug.Log("��й�ȣ ����ġ");
            PhotonNetwork.LeaveRoom();
            StartCoroutine(JoinRoomFail());
        }
    }
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
    }    
}
