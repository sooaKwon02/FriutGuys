using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Animator cubeAnimator;

    public GameObject effect;

    private void Awake()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CubeShake());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            cubeRenderer.material.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private IEnumerator CubeShake()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
        cubeAnimator.SetBool("Shake", true);
        yield return new WaitForSeconds(1.0f);
        cubeAnimator.SetBool("Shake", false);
        StopCoroutine(CubeShake());
        yield return StartCoroutine(CubeShake());
    }
}