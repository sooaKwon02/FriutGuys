using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.PlayerSettings;

public class GameEndManager : MonoBehaviour
{
    public GameObject spawnTile;
    public GameObject groundTile;
    public Image panel;

    public GameObject[] pos = new GameObject[16];
    GameObject[] player;
    public int count;

    private void Start()
    {
        panel.gameObject.SetActive(true);

        for (int i = 0; i < 16; i++)
        {
            if (i < 8)
            {
                GameObject obj = Instantiate(spawnTile, new Vector3(i * 3, 0, 0), Quaternion.identity);
                pos[i] = obj;
            }
            else
            {
                GameObject obj = Instantiate(spawnTile, new Vector3((i * 3) - 24, 3, 0), Quaternion.identity);
                pos[i] = obj;
            }
        }

        for (int i = -1; i < 9; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Instantiate(groundTile, new Vector3(i * 3, -10, j * -2), Quaternion.identity);
            }
        }
        StartCoroutine(JoinPlayer());
    }

    IEnumerator JoinPlayer()
    {
        yield return new WaitForSeconds(1f);
        while (GameObject.FindGameObjectsWithTag("Player").Length != PhotonNetwork.CurrentRoom.PlayerCount)
        {
            yield return null;
        }
        player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<PlayerCtrl>().enabled = false;
            player[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            player[i].GetComponent<PlayerCtrl>().startGame = false;
            player[i].transform.position = pos[i].transform.position;
            if (player[i].GetComponent<PlayerCtrl>().isGoalin)
            {
                count++;
            }
            else if (player[i].GetComponent<PlayerCtrl>().isAlive)
            {
                count++;
            }
            else
            {
                StartCoroutine(PlayerGameOver(pos[i]));
            }
            yield return new WaitForSeconds(0.1f);
        }
        panel.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(PlayerReady());
    }
    IEnumerator PlayerGameOver(GameObject pos)
    {
        yield return new WaitForSeconds(0.5f);
        pos.GetComponentInChildren<GameOver>().Over();
    }
    IEnumerator PlayerReady()
    {
        while (count != PhotonNetwork.CurrentRoom.PlayerCount)
        {
            yield return null;
        }
        count = 0;
        while (!PhotonNetwork.IsMasterClient)
        {
            yield return null;
        }  
        if (PhotonNetwork.IsMasterClient)
        {
            ScenesManager pc = FindObjectOfType<ScenesManager>();
            pc.LoadRandomScene();
        }
    }
}
