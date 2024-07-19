using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    SoundManager soundManager;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    public Text musicName;
    public Slider volume;
    public GameObject musicPanel;
    public GameObject musicSet;
    public int musicIndex;
    bool check;
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();  
    }
    private void Start()
    {
        musicName.text = soundManager.audioClips[musicIndex].name;
        musicPanel.gameObject.SetActive(false);
        audioSource=soundManager.audioSource;
        for (int i=0;i<soundManager.audioClips.Length;i++)
        {
            GameObject obj = Instantiate(musicSet);
            obj.GetComponent<MusicSet>().MusicSetting(soundManager.audioClips[i], audioSource);
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.SetParent(musicPanel.GetComponent<RectTransform>(),false);
            musicIndex++;
        }
        musicPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(450f, 85f * musicIndex);
        volume.value = audioSource.volume;
    }
    public void VolumeSet()
    {
        if(audioSource != null)
        {
            audioSource.volume = volume.value;
        }
    }
    public void Mute()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
        }             
    }
    public void MusicMenu()
    {
        check = !check;
        musicPanel.SetActive(check);     
    }
    
}
