using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("SoundManager");
                    _instance = singletonObject.AddComponent<SoundManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();       
        PlayClip();
        audioSource.Play();
    }
            
    void PlayClip()
    {
        audioSource.clip = audioClips[0];
    } 
}
