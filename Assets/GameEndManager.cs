using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    public GameObject spawnTile;
    public GameObject groundTile;
    public Image panel;
    public Camera camera;

    public GameObject[] pos = new GameObject[16];
    PlayerCtrl[] players;
    public int count;
    
    private void Start()
    {
        count = 0;
        panel.gameObject.SetActive(true);

        InitializeTiles();
        StartCoroutine(JoinPlayer());
    }
    private void InitializeTiles()
    {
        // Initialize spawn tiles
        for (int i = 0; i < 16; i++)
        {
            Vector3 position = i < 8 ? new Vector3(i * 3, 0, 0) : new Vector3((i * 3) - 24, 3, 0);
            pos[i] = Instantiate(spawnTile, position, Quaternion.identity);
        }

        // Initialize ground tiles
        for (int i = -1; i < 9; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Instantiate(groundTile, new Vector3(i * 3, -10, j * -2), Quaternion.identity);
            }
        }
    }
    IEnumerator JoinPlayer()
    {
        yield return new WaitForSeconds(1f);
        while (GameObject.FindGameObjectsWithTag("Player").Length != PhotonNetwork.CurrentRoom.PlayerCount)
        {
            yield return null;
        }
        players = FindObjectsOfType<PlayerCtrl>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].enabled = false;
            players[i].rb.velocity = Vector3.zero;
            players[i].startGame = false;
            players[i].transform.position = pos[i].transform.position;
            players[i].GetComponentInChildren<ThrowUp>().transform.rotation = Quaternion.Euler(0,180,0);
            players[i].anim.SetTrigger("Result");
            if (players[i].isGoalin|| players[i].isAlive)
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
        yield return new WaitForSeconds(5f);      
        StartCoroutine(PlayerReady());
    }
    IEnumerator PlayerGameOver(GameObject pos)
    {
        yield return new WaitForSeconds(0.5f);
        pos.GetComponentInChildren<GameOver>().Over();
    }
    IEnumerator PlayerReady()
    {    
        while (PhotonNetwork.MasterClient==null||count!=PhotonNetwork.CurrentRoom.PlayerCount)
        {
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(GameSet());
    }
    IEnumerator GameSet()
    {
        if (PhotonNetwork.IsMasterClient && count != 1)
        {
            ScenesManager pc = FindObjectOfType<ScenesManager>();
            pc.LoadRandomScene();
        }
        else if (PhotonNetwork.IsMasterClient && count == 1)
        {
            PlayerCtrl pc = FindObjectOfType<PlayerCtrl>();
            camera.transform.position = pc.transform.position + new Vector3(0, 1, -5);
            camera.transform.LookAt(pc.transform.position);
            yield return new WaitForSeconds(0.5f);
            pc.anim.SetTrigger("Victory");
            FindObjectOfType<ScenesManager>().count++;
            yield return new WaitForSeconds(3f);
            pc.GameOver();
        }
        yield return null;
    }
}
