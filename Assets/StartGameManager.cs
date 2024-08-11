using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    int currentPlayers;
    public Transform[] pos;
    public Collider goal;

    public int goalCount;
    public int count;
    GameObject[] players;
    PlayerCtrl playerCtrl;

    private Text gameTxt;

    int watchIndex = -1;

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
        goalCount = currentPlayers / 2;

        StartCoroutine(SpawnSet());
        StartCoroutine(GameCount());
        Invoke("GameStart", 3.5f);
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

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);

        //PhotonView[] pview = FindObjectsOfType<PhotonView>();
        //foreach (PhotonView pv in pview)
        //{
        //    if (pv.GetComponent<PlayerCtrl>().isGoalin)
        //    {
        //        //if (PhotonNetwork.IsMasterClient)
        //        //{
        //        //    PhotonNetwork.CloseConnection(player.GetComponent<PhotonView>().Owner);
        //        //}
        //        //else
        //        //{
        //        //    PhotonNetwork.LeaveRoom();
        //        // 마스터 클라이언트가 변경되었을때 콜백
        //        // public override void OnMasterClientSwitched(Player newMasterClient)
        //        PhotonNetwork.LoadLevel(5);
        //        //SceneManager.LoadScene(5);
        //    }
        //    else if(!pv.GetComponent<PlayerCtrl>().isGoalin)
        //    {
        //        //Destroy(cam.gameObject);
        //        PhotonNetwork.LeaveRoom();
        //    }
        //}
        if (goalCount != 1)
        {
            foreach (GameObject player in players)
            {
                PlayerCtrl playerCtrl = player.GetComponent<PlayerCtrl>();

                if (player.GetComponent<PhotonView>().IsMine)
                {
                    if (!playerCtrl.isGoalin)
                    {
                        PhotonNetwork.LeaveRoom();
                    }
                    else if (playerCtrl.isGoalin)
                    {
                        yield return new WaitForSeconds(2f);
                        playerCtrl.gameObject.SetActive(true);
                        PhotonNetwork.LoadLevel(5);
                    }
                }
            }
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    IEnumerator GameoverMsg()
    {
        gameTxt.enabled = true;
        gameTxt.text = "라운드 종료";
        yield return new WaitForSeconds(3f);

        foreach (GameObject player in players)
        {
            PlayerCtrl playerCtrl = player.GetComponent<PlayerCtrl>();

            if (player.GetComponent<PhotonView>().IsMine)
            {
                if (playerCtrl.isGoalin)
                {
                    gameTxt.text = "성공!";
                }
                else
                {
                    gameTxt.text = "실패!";
                }
            }
        }

        StartCoroutine(GameOver());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("SlideCollider"))
        {
            count++;
            other.GetComponent<PlayerCtrl>().isGoalin = true;
            //other.GetComponent<PlayerCtrl>().AllStop();  //얘가 문제임

            if (count >= goalCount)
            {
                StartCoroutine(GameoverMsg());
            }
        }
    }
}
