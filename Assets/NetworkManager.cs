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
        Debug.Log("�����ͷ� ����");
        base.OnConnectedToMaster();
    }
    public void OnClickCreateRoom()
    {
        // �� ���� �ɼ� ����
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
                    roomOptions.CustomRoomPropertiesForLobby = new string[] { "pwd" }; // �κ� ��й�ȣ �Ӽ� ǥ��
                    roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable(); // Hashtable �ʱ�ȭ
                    roomOptions.CustomRoomProperties["pwd"] = passwordInput.text; // "pwd"��� Ű�� ��й�ȣ ����
                    PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);
                    Debug.Log(roomNameInput.text);
                    Debug.Log(Convert.ToByte(fullRoomInput.text));
                    Debug.Log(roomOptions);
                    StartCoroutine(LoadStage());
                }
                else
                {
                    Debug.Log("��� �Է�");
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
            Debug.Log("�ο��� üũ");
        }
    }
    IEnumerator LoadStage()
    {
        PhotonNetwork.IsMessageQueueRunning = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync(3);
        yield return ao;
        Debug.Log("�ε� �Ϸ�");
    }
    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

}
