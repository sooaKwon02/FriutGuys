using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject UIScore;
    public TextMeshProUGUI UIScoreText;
    public GameObject UIHit;
    public Image UIHitImage;
    public TextMeshProUGUI UIHitText;

    private int money = 0;

    public static UIController instance;

    private void Awake()
    {
        instance = this;
        UIScore = GameObject.FindGameObjectWithTag("UI Panel");
        UIScoreText = UIScore.GetComponentInChildren<TextMeshProUGUI>();
        UIHit = GameObject.FindGameObjectWithTag("UI Panel Hit");
        UIHitImage = UIHit.GetComponentInChildren<Image>();
        UIHitText = UIHit.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        UIHit.SetActive(false);
    }

    private void Update()
    {
        UIScoreText.text = "GameMoney : " + money;
    }

    public void VRGameExit()
    {
        SaveLoad.instance.player.cashMoney += money;
        SaveLoad.instance.SaveData();
        SceneManager.LoadScene(2);
    }

    public IEnumerator HitRoutine()
    {
        money -= 1;
        UIHitImage.enabled = true;
        UIHitText.enabled = true;
        yield return new WaitForSeconds(0.25f);
        UIHitImage.enabled = false;
        UIHitText.enabled = false;
    }

    public void CivilianScore()
    {
        money += 10;
    }

    public void PoliceOfficerScore()
    {
        money += 100;
    }
}