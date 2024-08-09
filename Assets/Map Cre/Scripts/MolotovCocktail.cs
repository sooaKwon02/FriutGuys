using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovCocktail : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Civilian"))
        {
            Instantiate(ExplosiveController.instance.GetExplosive(transform.position));
            gameObject.SetActive(false);
        }

        else if (collision.collider.CompareTag("Police Officer"))
        {
            Instantiate(ExplosiveController.instance.GetExplosive(transform.position));
            gameObject.SetActive(false);
        }

        else if (collision.collider.CompareTag("Shield")|| collision.collider.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}