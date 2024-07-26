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
        Debug.Log("�����ͷ� ����");
        base.OnConnectedToMaster();
    }
    public void OnClickCreateRoom()
    {
        // �� ���� �ɼ� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;  // ���� ���� �ִ��� ����
        roomOptions.IsVisible = true;  // ���� �κ� ���̴��� ����
        roomOptions.MaxPlayers = Convert.ToByte(fullRoomInput.text);  // �ִ� �÷��̾� �� ����

        if (!string.IsNullOrEmpty(passwordInput.text))
        {
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "pwd" }; // �κ� ��й�ȣ �Ӽ� ǥ��
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable(); // Hashtable �ʱ�ȭ
            roomOptions.CustomRoomProperties["pwd"] = passwordInput.text; // "pwd"��� Ű�� ��й�ȣ ����
        }
        // �� ���� ��û
        PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions, TypedLobby.Default);
        Debug.Log("�� ���� �Ϸ�");
        StartCoroutine(LoadStage());
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
