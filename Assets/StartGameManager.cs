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
        Camera.main.gameObject.SetActive(true);
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
            players[i].GetComponent<PlayerCtrl>().enabled = true;
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
            playerCtrls.enabled = true;
            playerCtrls.moveSpeed = 5.0f;
            playerCtrls.isColl = false;
            playerCtrls.startGame = true;
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);

        Camera.main.gameObject.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(10);
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
        if (other.CompareTag("Player") || other.CompareTag("SlideCollider"))
        {
            if (count < goalCount)
            {
                count++;
                other.GetComponent<PlayerCtrl>().isGoalin = true;
            }
            if (count == goalCount)
            {
                StartCoroutine(GameoverMsg());
            }
        }
    }
}
