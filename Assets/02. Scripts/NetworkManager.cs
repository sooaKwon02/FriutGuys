using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField roomNameInput;
    public InputField fullRoomInput;
    public InputField passwordInput;
    public InputField inputPw;

    public bool secretCheck;
    public Toggle toggle;
    public Transform roomListPanel;

    public GameObject roomListButtonPrefabs;
    public GameObject createRoomFailPanel;
    public GameObject pwFailPanel;
    public GameObject scrollContents;
    public GameObject pwPanel;

    private string roomPassword = "";
    public List<RoomInfo> gameRoomList = new List<RoomInfo>();
    List<RoomInfo> re_list =new List<RoomInfo>();

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

        SaveLoad saveLoadScript = FindObjectOfType<SaveLoad>();
        
        PhotonNetwork.NickName = saveLoadScript.nickName;
    }
    private void Start()
    {
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
            if(toggle.isOn)
            {
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "Password", passwordInput.text } };
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "Password" };
            }
            bool isSucces = PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);

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


    private void OnPasswordEntered(string enteredPassword, RoomInfo room)
    {
        if (enteredPassword == roomPassword)
        {
            pwPanel.gameObject.SetActive(false);
            PhotonNetwork.JoinRoom(room.Name);
        }
        else
        {
            pwPanel.gameObject.SetActive(false);
            StartCoroutine(PWFail());
        }
    }

    IEnumerator PWFail()
    {
        pwFailPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        pwFailPanel.SetActive(false);
    }
    public void OnClickJoinRandomRoom()
    {
        //비밀번호 있으면 비공개 --> roomoption false?로 해서 리스트에만 보이게 할 수 있지 않을까 
        //준비 다 하고 시작한 방은 들어가지 못하게 --> 어차피 인원차면 못들어감
        JoinRandomRoomNoPw();
    }

    private void JoinRandomRoomNoPw() 
    {
        List<RoomInfo> roomsNoPassword = new List<RoomInfo>();

        // 비밀번호가 없는 방 필터링
        foreach (RoomInfo room in gameRoomList)
        {
            if (!room.CustomProperties.ContainsKey("Password"))
            {
                roomsNoPassword.Add(room);
            }
        }

        if (roomsNoPassword.Count > 0)
        {
            // 랜덤으로 방 선택
            RoomInfo selectedRoom = roomsNoPassword[Random.Range(0, roomsNoPassword.Count)];
            PhotonNetwork.JoinRoom(selectedRoom.Name);
        }
        else
        {
            StartCoroutine(JoinRoomFail());
        }

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        StartCoroutine(JoinRoomFail());
    }

    IEnumerator JoinRoomFail()
    {
        createRoomFailPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        createRoomFailPanel.SetActive(false);
    }

    IEnumerator RoomFull()
    {
        createRoomFailPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        createRoomFailPanel.SetActive(false);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomData obj in scrollContents.transform.GetComponentsInChildren<RoomData>())
        {
            Destroy(obj.gameObject);
        }
        HashSet<string> existingRoomNames = new HashSet<string>(gameRoomList.Select(r => r.Name));

        foreach (RoomInfo rm in roomList)
        {
            if (rm.PlayerCount > 0)
            {
                RoomInfo existingRoom = gameRoomList.FirstOrDefault(r => r.Name == rm.Name);
                if (existingRoom != null)
                {
                    if (existingRoom.PlayerCount != rm.PlayerCount)
                    {
                        gameRoomList.Remove(existingRoom);
                        gameRoomList.Add(rm);
                    }
                }
                else
                {
                    gameRoomList.Add(rm);
                }
            }
            else
            {
                RoomInfo roomToRemove = gameRoomList.FirstOrDefault(r => r.Name == rm.Name);
                if (roomToRemove != null)
                {
                    gameRoomList.Remove(roomToRemove);
                }
            }
        }
        foreach (RoomInfo _room in gameRoomList)
        {
            GameObject room = (GameObject)Instantiate(roomListButtonPrefabs);
            room.transform.SetParent(scrollContents.transform, false);
            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.Name;
            roomData.connectPlayer = _room.PlayerCount;
            roomData.maxPlayers = _room.MaxPlayers;
            roomData.DisplayRoomData();
            roomData.GetComponent<Button>().onClick.AddListener(delegate { OnClickRoomItem(_room); });
        }

        StartCoroutine(RoomListRectSet());
    }
    IEnumerator RoomListRectSet()
    {
        yield return new WaitForSeconds(0.3f);
        scrollContents.GetComponent<RectTransform>().pivot = new Vector3(0.5f, 1.0f);
        scrollContents.GetComponent<RectTransform>().sizeDelta = new Vector2(
        scrollContents.GetComponent<GridLayoutGroup>().cellSize.x,
        scrollContents.GetComponent<GridLayoutGroup>().cellSize.y * scrollContents.transform.childCount);
    }
    void OnClickRoomItem(RoomInfo roomInfo)
    {
        if (roomInfo.CustomProperties.TryGetValue("Password", out object password))
        {
            roomPassword = password as string;
            if (!string.IsNullOrEmpty(roomPassword))
            {
                pwPanel.gameObject.SetActive(true);
                inputPw.onEndEdit.AddListener(enteredPassword => OnPasswordEntered(enteredPassword, roomInfo));
            }
        }
        else
        {
            pwPanel.gameObject.SetActive(false);
            PhotonNetwork.JoinRoom(roomInfo.Name);
        }
    }
}