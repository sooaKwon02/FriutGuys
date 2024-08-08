using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : UseItem
{
    PhotonView pvMine;
    public float explosionRadius = 20f; 
    public float explosionForce = 700f;
    private void Awake()
    {
        pvMine = GetComponent<PhotonView>();
    }
    protected override void Start()
    {
        StartCoroutine(Explosion());
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        PhotonView pv = PV(collision);
        if (pvMine.Controller != pv.Controller && pv.CompareTag("Player"))
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
