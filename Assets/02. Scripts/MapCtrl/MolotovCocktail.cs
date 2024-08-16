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
            UIController.instance.CivilianScore();
            gameObject.SetActive(false);
        }

        else if (collision.collider.CompareTag("Police Officer"))
        {
            Instantiate(ExplosiveController.instance.GetExplosive(transform.position));
            UIController.instance.PoliceOfficerScore();
            gameObject.SetActive(false);
        }

        else if (collision.collider.CompareTag("Shield"))
        {
            Instantiate(ExplosiveController.instance.GetExplosive(transform.position));
            collision.collider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        else if (collision.collider.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}