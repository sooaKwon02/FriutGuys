using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject over;
    public void Win()
    {
        over.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0, 1);
    }
    public void Over()
    {
        StartCoroutine(OverGame());
    }
    IEnumerator OverGame()
    {
        
        over.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1);
        for(int i=0;i<20;i++)
        {
            yield return new WaitForSeconds(0.1f);
            over.transform.position+=new Vector3(0,0,0.1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerCtrl>().GameOver();
        }
    }
}
