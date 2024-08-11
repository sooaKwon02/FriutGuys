using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSet : MonoBehaviour
{
    public Slider musicVolume;
    public Text musicName;
    public Transform selectPanel;
    public Transform musicContents;
    public GameObject musicItem;
    AudioSource[] audioSources;
    public Toggle toggle;
    GameObject music;

    private void Awake()
    {
        audioSources = SoundManager.Instance.audioSources;
        musicVolume.value = audioSources[0].volume;
        toggle.isOn = audioSources[0].mute;
        musicName.text = audioSources[0].clip.name;
    }
    private void Start()
    {
        for (int i = 0; i < SoundManager.Instance.backgroundSound.Length; i++)
        {
            music = Instantiate(musicItem);
            music.transform.SetParent(musicContents, false);
            music.GetComponent<MusicItem>().Setting(SoundManager.Instance.backgroundSound[i].name, i);
        }
        musicContents.GetComponent<RectTransform>().sizeDelta = new Vector2(music.GetComponent<RectTransform>().sizeDelta.x,
                      SoundManager.Instance.backgroundSound.Length *( music.GetComponent<RectTransform>().sizeDelta.y));
        selectPanel.gameObject.SetActive(false);
    }

    public void SelectPanelOnOff()
    {
        if(selectPanel.gameObject.activeSelf)
        {
            selectPanel.gameObject.SetActive(false);
        }
        else 
        {
            selectPanel.gameObject.SetActive(true);           
        }
    }
    public void SetBackgroundMusicVolume(Slider volume)
    {
        SoundManager.Instance.backgroundVolume = volume.value;
        foreach(AudioSource aud in audioSources)
        {
            aud.volume = volume.value;
        }
    }
    public void SetMuted(Toggle mute)
    {
        if(mute.isOn)
        {
            SoundManager.Instance.isMuted = true;
            foreach (AudioSource aud in audioSources)
            {
                aud.mute = true;
            }               
        }        
        else
        {
            SoundManager.Instance.isMuted = false;
            foreach (AudioSource aud in audioSources)
            {
                aud.mute = false;
            }                
        }            
    }  
    public void SaveSetting()
    {
        SoundManager.Instance.SaveSettings();
        GameManager gameManager=FindObjectOfType<GameManager>();
        StartCoroutine(gameManager.ErrorSend("저장되었습니다."));
        selectPanel.gameObject.SetActive(false);
    }
}
