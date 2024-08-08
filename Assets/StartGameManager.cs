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

    private void Update()
    {
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
            else if(!pv.GetComponent<PlayerCtrl>().isGoalin)
            {
                //Destroy(cam.gameObject);
                PhotonNetwork.LeaveRoom();
            }
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            count++;
            other.GetComponent<PlayerCtrl>().isGoalin = true;
            PhotonView[] pv = FindObjectsOfType<PhotonView>();
            
            foreach (PhotonView pview in pv)
            {
                //내 카메라 찾고
                if (pview.IsMine && pview.GetComponent<PlayerCtrl>().isGoalin)
                {
                    Camera.main.transform.parent = null;
                }
            }
            //Camera.main.transform.parent = null;
            if (count >= goalCount)
            {
                StartCoroutine(GameoverMsg());
                StartCoroutine(OffPlayer(other));
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
        PhotonView[] pv = FindObjectsOfType<PhotonView>();
        foreach(PhotonView pvs in pv)
        {
            if (!pvs.GetComponent<PlayerCtrl>().isGoalin)
            {
                return;
            }
        }

        Camera.main.transform.position = Vector3.zero;
        Camera.main.transform.rotation = Quaternion.identity;
        //PhotonView[] pv = FindObjectsOfType<PhotonView>();

        //원형배열
        int startIndex = (watchIndex + 1) % pv.Length;
        for (int i = 0; i < pv.Length; i++)
        {
            int index = (startIndex + i) % pv.Length;

            if (!pv[index].GetComponent<PlayerCtrl>().isGoalin)
            {
                PhotonView nextPlayer = pv[index];
                if (nextPlayer.GetComponent<PlayerCtrl>().cam != null)
                {
                    Camera.main.transform.SetParent(nextPlayer.GetComponent<PlayerCtrl>().cam.transform);
                    Camera.main.transform.localPosition = new Vector3(0, 4f, -6.15f);
                    Camera.main.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
                    watchIndex = index;
                    return;
                }
            }
        }
    }
}
