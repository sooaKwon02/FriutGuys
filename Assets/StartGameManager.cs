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

    int goalCount;
    int count;
    GameObject[] players;

    private Text gameTxt;

    int watchIndex = -1;
    Camera cam;

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

    private void Update()
    {
        //if (count >= goalCount)
        //{
        //    StartCoroutine(GameoverMsg());
        //}

        if (Input.GetMouseButtonDown(0))
        {
            Watching();
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
                yield return new WaitForSeconds(1f);
                gameTxt.text = "";
                ShowMessage(playerScript, "탈락!");
            }
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);

        PhotonView[] pview = FindObjectsOfType<PhotonView>();
        foreach (PhotonView pv in pview)
        {
            if (pv.GetComponent<PlayerCtrl>().isGoalin)
            {
                //if (PhotonNetwork.IsMasterClient)
                //{
                //    PhotonNetwork.CloseConnection(player.GetComponent<PhotonView>().Owner);
                //}
                //else
                //{
                //    PhotonNetwork.LeaveRoom();
                // 마스터 클라이언트가 변경되었을때 콜백
                // public override void OnMasterClientSwitched(Player newMasterClient)
                PhotonNetwork.LoadLevel(5);
                //SceneManager.LoadScene(5);
            }
            else
            {
                PhotonNetwork.LeaveRoom();
            }
        }
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
                
                StartCoroutine(OffPlayer(other));
            }
            if (count >= goalCount)
            {
                StartCoroutine(GameoverMsg());
                StartCoroutine(GameOver());
            }
        }
    }

    IEnumerator OffPlayer(Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        other.gameObject.SetActive(false);
    }

    void Watching()
    {
        //카메라 값 초기화
        cam.transform.position = Vector3.zero;
        cam.transform.rotation = Quaternion.identity;

        PhotonView[] pv = FindObjectsOfType<PhotonView>();
        int startIndex = (watchIndex + 1) % pv.Length;
        for (int i = 0; i < pv.Length; i++)
        {
            //원형배열
            int index = (startIndex + i) % pv.Length;

            //if (pv[index].gameObject.activeSelf)
            if (!pv[index].GetComponent<PlayerCtrl>().isGoalin)
            {
                PhotonView nextPlayer = pv[index];
                if (nextPlayer.GetComponent<PlayerCtrl>().cam != null)
                {
                    cam.transform.SetParent(nextPlayer.GetComponent<PlayerCtrl>().cam.transform);
                    cam.transform.localPosition = new Vector3(0, 4f, -6.15f);
                    cam.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
                    //cam.transform.localPosition = new Vector3(0, -0.71f, -6.15f);
                    //cam.transform.rotation = Quaternion.Euler(40f, 0f, 0f); -->안해도됨 어차피 000이라
                    watchIndex = index;
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
            scCtrl.playerTxt.transform.parent.gameObject.SetActive(true);
            scCtrl.playerTxt.text = msg;
        }
    }
}
