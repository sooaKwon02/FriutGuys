using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSetting : MonoBehaviour
{
    public Text screenModeText;
    public GameObject sizePanel;
    public Mask mask;
    public Text sizeText;
    public RectTransform uiElement;

    private int sizeX;
    private int sizeY;
    private bool on = true;

    private const string ScreenSizeKey = "Screen";
    private const string SizeXKey = "sizeX";
    private const string SizeYKey = "sizeY";
    
    private void Start()
    {
        mask.enabled = true;
        LoadSettings();
        RectSizeChange(sizeX, sizeY, on);
    }

    public void OnSize()
    {
        on = !on;
        screenModeText.text = on ? "전체 화면" : "창 화면";
        RectSizeChange(sizeX, sizeY, on);
        SaveSettings(); 
    }

    public void LoadSettings()
    {
        sizeX = PlayerPrefs.GetInt(SizeXKey, 1920); // Default to 1920 if not set
        sizeY = PlayerPrefs.GetInt(SizeYKey, 1080); // Default to 1080 if not set
        on = PlayerPrefs.GetInt(ScreenSizeKey, 1) == 1; // Default to fullscreen if not set

        // Update UI elements to match loaded settings
        screenModeText.text = on ? "전체 화면" : "창 화면";
        sizeText.text = $"{sizeX} | {sizeY}";
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt(SizeXKey, sizeX);
        PlayerPrefs.SetInt(SizeYKey, sizeY);
        PlayerPrefs.SetInt(ScreenSizeKey, on ? 1 : 0);
        PlayerPrefs.Save(); 
    }
    public void SaveSetting()
    {
        SaveSettings();
        GameManager gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(gameManager.ErrorSend("저장되었습니다."));
    }

    public void SizePanelOpen()
    {
        sizePanel.SetActive(!sizePanel.activeSelf);
    }

    private void RectSizeChange(int screenX, int screenY, bool fullscreen)
    {
        Screen.SetResolution(screenX, screenY, fullscreen);
    }

    public void MaskOnOff()
    {
        mask.enabled = !mask.enabled;
    }

    public void SizeButton(int num)
    {
        switch (num)
        {
            case 0:
                RectSizeChange(1280, 720, on);
                sizeX = 1280;
                sizeY = 720;
                sizeText.text = "1280 | 720";
                break;
            case 1:
                RectSizeChange(1366, 768, on);
                sizeX = 1366;
                sizeY = 768;
                sizeText.text = "1366 | 768";
                break;
            case 2:
                RectSizeChange(1600, 900, on);
                sizeX = 1600;
                sizeY = 900;
                sizeText.text = "1600 | 900";
                break;
            case 3:
                RectSizeChange(1920, 1080, on);
                sizeX = 1920;
                sizeY = 1080;
                sizeText.text = "1920 | 1080";
                break;
            default:
                break;
        }
        SaveSettings();
    }
}
