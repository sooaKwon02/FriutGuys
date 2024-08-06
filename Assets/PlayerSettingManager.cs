using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSettingManager : MonoBehaviourPunCallbacks
{
    public static PlayerSettingManager Instance { get; private set; }
    private string playerName;
    public string version = "0.1.0f";
    public enum GAME_TYPE
    {
        RACING,
        BATTLE,
    }
    public GAME_TYPE type;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DatabaseManager dbManager = FindObjectOfType<DatabaseManager>();
        if (dbManager != null)
        {
            playerName = dbManager.nicknametext;
            PhotonNetwork.NickName = playerName;
        }
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = version;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    //πÊ¡¢º”====================================================
   
    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(LoadSceneAsync(3));
        }
    }   
    
    public override void OnLeftRoom()
    {
        StartCoroutine(LoadSceneAsync(2));
        foreach (var obj in FindObjectsOfType<PhotonView>())
        {
            if (obj.IsMine)
            {
                PhotonNetwork.Destroy(obj.gameObject);
            }
        }
    }  

    private IEnumerator LoadSceneAsync(int sceneNum)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
