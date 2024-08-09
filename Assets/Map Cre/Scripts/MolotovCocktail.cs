using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovCocktail : MonoBehaviour
{
    private ScoreController score;

    private void Awake()
    {
        score = GameObject.Find("UI Text Score").GetComponent<ScoreController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Civilian"))
        {
            Instantiate(ExplosiveController.instance.GetExplosive(transform.position));
            score.score += 1;
            gameObject.SetActive(false);
        }

        else if (collision.collider.CompareTag("Police Officer"))
        {
            Instantiate(ExplosiveController.instance.GetExplosive(transform.position));
            score.score += 2;
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