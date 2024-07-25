using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();       
        PlayClip();
        audioSource.Play();
    }
            
    void PlayClip()
    {
        audioSource.clip = audioClips[0];
    } 
}
