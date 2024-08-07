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

    private Text gameTxt;

    int watchIndex = -1;
    Camera cam;
    //int index;
    //PlayerCtrl nextPlayer;
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

        gameTxt.transform.SetParent(canvas.transform);

        players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach(GameObject player in players)
        {
            player.SetActive(true);
        }

        count = 0;
        goalCount = currentPlayers;

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

        if(cam.transform.parent == null)
        {
            Watching();
            //cam.transform.position = new Vector3(0, -0.71f, -6.15f);
            //cam.transform.rotation = nextPlayer.cam.transform.rotation;
            //cam.transform.SetParent(nextPlayer.cam.transform);
            //cam.transform.localPosition = new Vector3(0, -0.71f, -6.15f);
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

                cam = other.gameObject.GetComponentInChildren<Camera>();
                cam.gameObject.transform.parent = null;
                cam.transform.position = Vector3.zero;
                cam.transform.rotation = Quaternion.identity;
                
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

    void Watching()
    {
        //카메라를 떼어서 
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Watching");
            //포톤뷰로 찾아오는걸 구현해봐
            PlayerCtrl[] playersCtrl = FindObjectsOfType<PlayerCtrl>();

            //원형배열
            int startIndex = (watchIndex + 1) % playersCtrl.Length;
            for (int i = 0; i < playersCtrl.Length; i++)
            {
                int index = (startIndex + i) % playersCtrl.Length;

                if (!playersCtrl[index].isGoalin)
                {
                    PlayerCtrl nextPlayer = playersCtrl[index];
                    if (nextPlayer.cam != null)
                    {
                        cam.transform.SetParent(nextPlayer.cam.transform);
                        cam.transform.localPosition = new Vector3(0, -0.71f, -6.15f);
                        watchIndex = index;
                    }
                    return;
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
            scCtrl.playerTxt.text = msg;
        }
    }
}
