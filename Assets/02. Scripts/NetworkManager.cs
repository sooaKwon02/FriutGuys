using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;


public class NetworkManager : MonoBehaviourPunCallbacks
{
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
   
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        secretCheck = false;
        if(passwordInput != null)
        passwordInput.enabled= !secretCheck;
    }
    public void SecretCheck()
    {
        secretCheck = !secretCheck;
        passwordInput.text = null;
        passwordInput.enabled = !secretCheck;
    }    public void CreateRoom(bool check)
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
        roomOptions.IsOpen = true;  // 방이 열려 있는지 여부
        roomOptions.IsVisible = true;  // 방이 로비에 보이는지 여부
        roomOptions.MaxPlayers = Convert.ToByte(fullRoomInput.text);  // 최대 플레이어 수 설정

        if (!string.IsNullOrEmpty(passwordInput.text))
        {
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "pwd" }; // 로비에 비밀번호 속성 표시
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable(); // Hashtable 초기화
            roomOptions.CustomRoomProperties["pwd"] = passwordInput.text; // "pwd"라는 키로 비밀번호 설정
        }
        // 방 생성 요청
        PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);
        Debug.Log("방 생성 완료");
        StartCoroutine(LoadStage());
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
