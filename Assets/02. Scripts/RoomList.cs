
using Photon.Pun;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    public Text roomName;
    public Text countPeaple;
    public Image Secret;
    int count;
    string password;
    Button button;
    //===========================================================
    public GameObject passwordPanel;
    public InputField passwordInput;
    public GameObject failText;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnclickRoomEnter);
        failText.SetActive(false);
        password = null;
    }
    void OnclickRoomEnter()
    {        
            PhotonNetwork.JoinRoom(roomName.text);             
    }
    public void OnclickSecretRoomEnter()
    {
        if (password == passwordInput.text)
        {
            PhotonNetwork.JoinRoom(roomName.text);
        }
        else
        {
            StartCoroutine(Fail());
        }
    }
    IEnumerator Fail()
    {
        failText.SetActive(true);
        yield return new WaitForSeconds(2);
        failText.SetActive(false);
        passwordPanel.SetActive(false);
    }

    public void CreateRoomInfo(string _roomname,int connect,int full,bool _secret,string _password)
    {
        roomName.text = _roomname;
        countPeaple.text="("+ connect.ToString()+ "/"+full.ToString()+")";
        if (_secret) { Secret.enabled = true; password = _password; }
        else { Secret.enabled = false; }        
    }
}
