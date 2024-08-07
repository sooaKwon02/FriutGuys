using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    int currentPlayers;
    public Transform[] pos;
    public Collider goal;

    int goalCount;
    int count;
    GameObject[] players;
    GameObject error;

    private Text gameTxt;

    GameObject[] playerTxt;

    private void Awake()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 0)
        {
            currentPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        }
        gameTxt = GameObject.FindGameObjectWithTag("GAME_TXT").GetComponent<Text>();
    }
    private void Start()
    {
        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        //playerTxt = GameObject.FindGameObjectsWithTag("PLAYER_TXT");
        gameTxt.transform.SetParent(canvas.transform);

        players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach(GameObject player in players)
        {
            player.SetActive(true);
        }

        count = 0;
        goalCount = currentPlayers / 2;

        StartCoroutine(SpawnSet());
        StartCoroutine(GameCount());
        Invoke("GameStart", 3.5f);
    }

    private void Update()
    {
        if (count >= goalCount)
        {
            StartCoroutine(GameoverMsg());
        }
    }

    IEnumerator SpawnSet()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = pos[i].position;
        }
        yield return new WaitForSeconds(3f);
    }

    IEnumerator GameCount()
    {

        PlayerCtrl[] playerCtrl = FindObjectsOfType<PlayerCtrl>();

        foreach (PlayerCtrl playerCtrls in playerCtrl)
        {
            playerCtrls.moveSpeed = 0;
            playerCtrls.isColl = true;
        }

        for (int i = 3; i > 0; i--)
        {
            gameTxt.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        gameTxt.text = "Go!";

    }

    void GameStart()
    {
        gameTxt.enabled = false;
        PlayerCtrl[] playerCtrl = FindObjectsOfType<PlayerCtrl>();

        foreach (PlayerCtrl playerCtrls in playerCtrl)
        {
            playerCtrls.moveSpeed = 5.0f;
            playerCtrls.isColl = false;
        }
    }

    IEnumerator GameoverMsg()
    {
        gameTxt.text = "라운드 종료";

        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                PlayerCtrl playerScript = player.GetComponent<PlayerCtrl>();
                yield return new WaitForSeconds(0.5f);
                gameTxt.text = "";
                ShowMessage(playerScript, "탈락!");
            }
        }
    }
    IEnumerator GameOver()
    {
        foreach (GameObject player in players)
        {
           if(!player.GetComponent<PlayerCtrl>().isGoalin)
           {
                if (PhotonNetwork.IsMasterClient)
                {                    
                    PhotonNetwork.CloseConnection(player.GetComponent<PhotonView>().Owner);
                }
                else
                {
                    PhotonNetwork.LeaveRoom();
                }                
            }
        }
        yield return new WaitForSeconds(5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(count < goalCount)
            {
                count++;
                Debug.Log("count : " + count);
                PlayerCtrl playerCtrl = other.GetComponent<PlayerCtrl>();
                playerCtrl.isGoalin = true;

                ShowMessage(playerCtrl, "성공!");
                Watching(other);

                StartCoroutine(Active(other));
            }
            else
            {
                //StartCoroutine(GameOver());
            }
            
        }
    }

    IEnumerator Active(Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        other.gameObject.SetActive(false); 
    }

    int watchIndex = -1;
    void Watching(Collider other)
    {
        //카메라를 떼어서 
        Camera cam = other.gameObject.GetComponentInChildren<Camera>();
        //마우스 버튼을 누르면
        if (Input.GetMouseButtonDown(1))
        {
            PlayerCtrl[] playersCtrl = FindObjectsOfType<PlayerCtrl>();
                //PlayerCtrl nextPlayer = playersCtrl[nextNum];

                //foreach(PlayerCtrl players in playersCtrl)
                //{
                //    if (!players.isGoalin)
                //    {
                //        cam.transform.position = players.cam.transform.position;
                //        cam.transform.rotation = players.cam.transform.rotation;
                //    }
                //}
                int startIndex = (watchIndex + 1) % playersCtrl.Length;
                for (int i = 0; i < playersCtrl.Length; i++)
                {
                    int index = (startIndex + i) % playersCtrl.Length;
                    if (!playersCtrl[index].isGoalin) 
                    {
                        PlayerCtrl nextPlayer = playersCtrl[index];
                        cam.transform.position = nextPlayer.cam.transform.position;
                        cam.transform.rotation = nextPlayer.cam.transform.rotation;
                        watchIndex = index;
                    }
                }
            
        }
    }


    void ShowMessage(PlayerCtrl scCtrl, string msg)
    {
        if (scCtrl != null && scCtrl.pv.IsMine)
        {
            scCtrl.moveSpeed = 0f;
            scCtrl.isColl = true;
            scCtrl.playerTxt.transform.parent.gameObject.SetActive(true);
            scCtrl.playerTxt.text = msg;
        }
    }
}
