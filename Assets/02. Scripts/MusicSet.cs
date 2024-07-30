using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSet : MonoBehaviour
{
    AudioSetting setting;
    AudioSource musicSource;
    AudioClip musicClip;
    Text text;
    Button button;
    private void Awake()
    {
        setting = FindFirstObjectByType<AudioSetting>();
        text=GetComponentInChildren<Text>();
        button=GetComponent<Button>();  
    }
    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
        setting.musicName.text = text.text; 
    }
    public void MusicSetting(AudioClip clip,AudioSource audio)
    {
        musicSource = audio;
        musicClip = clip;
        text.text= musicClip.name; 
    }
}
