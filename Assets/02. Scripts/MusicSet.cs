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
    AudioSource audioSource;
    public Toggle toggle;

    private void Awake()
    {        
        audioSource =FindObjectOfType<AudioSource>();
        for(int i=0;i< SoundManager.Instance.backgroundSound.Length;i++)
        {
            GameObject music = Instantiate(musicItem); 
            music.transform.SetParent(musicContents, false);
            music.GetComponent<MusicItem>().Setting(SoundManager.Instance.backgroundSound[i].name, i);
        }
        musicContents.GetComponent<RectTransform>().sizeDelta = new Vector2(FindObjectOfType<MusicItem>().GetComponent<RectTransform>().sizeDelta.x,
                    FindObjectsOfType<MusicItem>().Length * FindObjectOfType<MusicItem>().GetComponent<RectTransform>().sizeDelta.y); ;

    }
    private void Start()
    {
        selectPanel.gameObject.SetActive(false);
        musicVolume.value = SoundManager.Instance.backgroundVolume;
        toggle.isOn = SoundManager.Instance.isMuted;
        musicName.text = audioSource.clip.name;

       
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
        audioSource.volume = volume.value   ;
    }
    public void SetMuted(Toggle mute)
    {
       if(mute.isOn)
        {
            SoundManager.Instance.isMuted = true;
            audioSource.mute = true;
        }        
        else
        {
            SoundManager.Instance.isMuted = false;
            audioSource.mute = false;
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
