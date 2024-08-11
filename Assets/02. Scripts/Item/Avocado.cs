using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Avocado : UseItem
{
    public float explosionRadius = 20f;
    public float explosionForce = 1500f;
    protected override void Awake()
    {
        base.Awake();
    }

    new void Start()
    {
        if (pv.IsMine)
        {
            StartCoroutine(Explosion());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (pv.IsMine)
        {
            pv.RPC("ExplodeRPC", RpcTarget.All);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(4f);
        pv.RPC("ExplodeRPC", RpcTarget.All);
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void ExplodeRPC()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                GetComponent<ParticleSystem>().Play();
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
