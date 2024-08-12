using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject over;
    public GameObject obj;  
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
        while(over.transform.localPosition.z>-0.2f)
        {
            over.transform.Translate(Vector3.back*0.02f,Space.World);
            yield return null;  
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            obj = collision.gameObject;
            collision.transform.GetComponent<PlayerCtrl>().GameOver();
        }
    }
}
