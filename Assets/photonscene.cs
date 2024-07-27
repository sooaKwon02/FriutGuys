using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photonscene : MonoBehaviourPunCallbacks
{
    string PlayerName;
    GameObject gamePlayer;
    private void Awake()
    {
        if (FindObjectOfType<DatabaseManager>())
        {
            PlayerName = FindObjectOfType<DatabaseManager>().nickname.text.ToString();
        }      
    }
  
    public override void OnJoinedRoom()
    {
        StartCoroutine(LoadSceneAsync(3));
    }
    public override void OnLeftRoom()
    {
        StartCoroutine(LoadSceneAsync(2));
    }
    private IEnumerator LoadSceneAsync(int SceneNum)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNum);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            yield return null;
        }

        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            gamePlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            GameObject userInfoPanel = GameObject.FindGameObjectWithTag("UserInfoPanel");
            if (userInfoPanel != null)
            {
                GameObject userinfo = PhotonNetwork.Instantiate("UserInfo", Vector3.zero, Quaternion.identity);
                userinfo.GetComponent<UserInfo>().SetUserInfo(PlayerName);
                userinfo.transform.SetParent(userInfoPanel.transform, false);

            }
        }
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














    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 입장했습니다.");
    }

    public override void OnLeftLobby()
    {
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("들어옴:" + newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("나감:" + otherPlayer.NickName);
    }
    public void KickPlayerByNickname(string playerNickname)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == playerNickname)
            {
                PhotonNetwork.CloseConnection(player);
                return;
            }
        }
    }
}
