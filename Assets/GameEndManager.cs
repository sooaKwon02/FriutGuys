using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameEndManager : MonoBehaviour
{
    public GameObject spawnTile;
    public GameObject groundTile;

    public Transform[] pos = new Transform[16];
    GameObject[] player;

    private void Start()
    {
        for(int i=0;i<16; i++)
        {
            if (i < 8) {
                Instantiate(spawnTile, new Vector3(i*2, 0, 0), Quaternion.identity);
                pos[i] = spawnTile.transform;
            }
            else
            {
                Instantiate(spawnTile, new Vector3((i*2)-16, 2, 0), Quaternion.identity);
                pos[i] = spawnTile.transform;
            }
                
           
        }      
       
        for (int i=-1;i<9;i++)
        {
            for(int j=0;j<3; j++)
            {
                Instantiate(groundTile, new Vector3(i * 2, -10, j*-2), Quaternion.identity);
            }
            
        }
        StartCoroutine(JoinPlayer());
    }

    IEnumerator JoinPlayer()
    {
        yield return new WaitForSeconds(1f);
        if(GameObject.FindGameObjectWithTag("Player")&&GameObject.FindGameObjectsWithTag("Player").Length==PhotonNetwork.CurrentRoom.PlayerCount)
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < player.Length; i++) 
            {
                player[i].transform.position = pos[i].position;
                if(!player[i].GetComponent<PlayerCtrl>().isGoalin)
                {
                    StartCoroutine(PlayerGameOver(pos[i]));
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            StartCoroutine(JoinPlayer());
        }      
    }
    IEnumerator PlayerGameOver(Transform pos)
    {
        yield return new WaitForSeconds(1f);
        pos.GetComponent<GameOver>().Over();       
    }  
}
