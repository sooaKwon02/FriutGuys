using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectsBullet : MonoBehaviour
{
    private ScoreController score;
    private GameObject hitController;
    private Image hitImage;
    private TextMeshProUGUI hitText;

    private readonly float moveSpeed = 10.0f;

    private void Awake()
    {
        score = GameObject.Find("UI Text Score").GetComponent<ScoreController>();
        hitController = GameObject.FindGameObjectWithTag("Hit Panel");
        hitImage = hitController.GetComponentInChildren<Image>();
        hitText = hitController.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        float moveZ = moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, moveZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HitRoutine());
        }

        else if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator HitRoutine()
    {
        score.score -= 1;
        hitImage.enabled = true;
        hitText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        hitImage.enabled = false;
        hitText.enabled = false;
        gameObject.SetActive(false);
    }
}