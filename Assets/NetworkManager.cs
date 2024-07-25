using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    private static NetworkManager _instance;
    public static NetworkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NetworkManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("NetworkManager");
                    _instance = singletonObject.AddComponent<NetworkManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    //=======================================================================
    public GameManager GameManager;
    public InputField roomNameInput;
    public InputField fullRoomInput;
    public InputField passwordInput;
    bool secretCheck;
    public GameObject createRoomPanel;
    public GameObject createRoom;
    //===================================================================
    public string version = "Ver 0.1.0";
    public PunLogLevel LogLevel = PunLogLevel.Full;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PhotonNetwork.ConnectUsingSettings();
        secretCheck = true;
        if (passwordInput != null)
            passwordInput.enabled = secretCheck;
    }
   
    public void SecretCheck()
    {
        secretCheck = !secretCheck;
        passwordInput.text = null;
        passwordInput.enabled = secretCheck;
    }
    public void CreateRoom(bool check)
    {
        createRoomPanel.SetActive(!check);
        createRoom.SetActive(check);
        GameManager.CreateRoomOnOff(check);
    }
    //=======================================================================================
    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터로 접속");
        base.OnConnectedToMaster();
    }
    public void OnClickCreateRoom()
    {
        // 방 설정 옵션 생성
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = secretCheck;
        roomOptions.IsVisible = true;
        if (Convert.ToByte(fullRoomInput.text) < 17)
        {
            roomOptions.MaxPlayers = Convert.ToByte(fullRoomInput.text);
            if (secretCheck)
            {
                if (!string.IsNullOrEmpty(passwordInput.text))
                {
                    roomOptions.CustomRoomPropertiesForLobby = new string[] { "pwd" }; // 로비에 비밀번호 속성 표시
                    roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable(); // Hashtable 초기화
                    roomOptions.CustomRoomProperties["pwd"] = passwordInput.text; // "pwd"라는 키로 비밀번호 설정
                    PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);
                    Debug.Log(roomNameInput.text);
                    Debug.Log(Convert.ToByte(fullRoomInput.text));
                    Debug.Log(roomOptions);
                    StartCoroutine(LoadStage());
                }
                else
                {
                    Debug.Log("비번 입력");
                }                
            }
            else
            {
                passwordInput.text = null;
                PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);
                Debug.Log(roomNameInput.text);
                Debug.Log(Convert.ToByte(fullRoomInput.text));
                Debug.Log(roomOptions);
                StartCoroutine(LoadStage());
            }
        }
        else
        {
            Debug.Log("인원수 체크");
        }
    }
    IEnumerator LoadStage()
    {
        PhotonNetwork.IsMessageQueueRunning = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync(3);
        yield return ao;
        Debug.Log("로딩 완료");
    }
    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

}
