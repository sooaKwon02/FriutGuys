using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleGameManager : MonoBehaviour
{
    int currentPlayers;
    public Transform[] pos;
    public Collider fallColl;

    public int fallCount;
    public int count;
    GameObject[] players;

    private Text gameTxt;

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

        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerCtrl>().isGoalin = false;
            player.SetActive(true);
        }

        count = currentPlayers;
        fallCount = currentPlayers / 2;

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

        ObstacleScript oS = FindObjectOfType<ObstacleScript>();
        ObstacleSpeed oSpeed = FindObjectOfType<ObstacleSpeed>();
        oS.speed = 0;
        oSpeed.speed = 0;
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

        ObstacleScript oS = FindObjectOfType<ObstacleScript>();
        ObstacleSpeed oSpeed = FindObjectOfType<ObstacleSpeed>();
        oS.speed = 25;
        oSpeed.speed = 25;

        foreach (PlayerCtrl playerCtrls in playerCtrl)
        {
            playerCtrls.moveSpeed = 5.0f;
            playerCtrls.isColl = false;
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);

        if (fallCount != 1)
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
                    gameTxt.text = "실패!";
                }
                else
                {
                    gameTxt.text = "성공!";
                }
            }
        }

        StartCoroutine(GameOver());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("SlideCollider"))
        {
            count--;
            other.GetComponent<PlayerCtrl>().isGoalin = true;
            //other.GetComponent<PlayerCtrl>().AllStop();

            if (count <= fallCount)
            {
                StartCoroutine(GameoverMsg());
            }
        }
    }
}
