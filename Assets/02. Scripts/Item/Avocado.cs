using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : UseItem
{
    public float explosionRadius = 20f; 
    public float explosionForce = 700f; 

    protected override void Start()
    {
        StartCoroutine(Explosion());
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        PhotonView pv;
        if (collision.transform.GetComponentInParent<PhotonView>())
        {
            pv = collision.gameObject.GetComponentInParent<PhotonView>();
        }
        else if (collision.transform.GetComponent<PhotonView>())
        {
            pv = collision.gameObject.GetComponent<PhotonView>();
        }
        else
        {
            pv = null;
        }
        if (pv != null && !pv.IsMine && pv.CompareTag("Player"))
        {
            Explode();
        }
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(4f);
        Explode();
    }
    void Explode()
    {      
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        Destroy(gameObject);
    }
}
