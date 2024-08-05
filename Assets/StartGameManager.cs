using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    int currentPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
    public Transform[] pos;
    public Collider goal;

    int goalCount;
    int count;
    GameObject[] players;
    GameObject error;
    private void Awake()
    {
        error = GameObject.FindGameObjectWithTag("ErrorBox");
    }
    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        count = 0;
        goalCount = currentPlayers / 2;
        StartCoroutine(SpawnSet());
    }
    IEnumerator SpawnSet()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = pos[i].position;
        }
        yield return new WaitForSeconds(3f);
    }

    IEnumerator GameStart()
    {
        error.SetActive(true);
        error.GetComponentInChildren<Text>().text = "게임시작";
        yield return new WaitForSeconds(10f);
    }
    IEnumerator GameOver()
    {
        foreach (GameObject player in players)
        {
           if(!player.GetComponent<CharacterCustom>().goalIn)
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
        yield return new WaitForSeconds(10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            if(count<goalCount)
            {
                count++;
                other.GetComponent<CharacterCustom>().goalIn = true;
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, other.GetComponent<Rigidbody>().velocity.y, 0);
                other.GetComponent<PlayerCtrl>().enabled = false;
            }
            else
            {
                StartCoroutine(GameOver());
            }
            
        }
    }
}
