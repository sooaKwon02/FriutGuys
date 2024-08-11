using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSources;
    public AudioClip[] backgroundSound;
    public AudioClip buttonClickSound;

    static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject(typeof(SoundManager).Name);
                _instance = singletonObject.AddComponent<SoundManager>();
                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    public float backgroundVolume = 1.0f;
    public bool isMuted = false;
    public int num=0;

    const string VolumeKey = "Volume";
    const string MuteKey = "Mute";
    const string BackgroundAudioKey = "Clip";

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadSettings(backgroundVolume, num, isMuted);
        SetAudio(backgroundVolume, num, isMuted);
    }

    public void SetAudio(float volume, int backgroundAudioIndex, bool mute)
    {

        audioSources.volume = volume;
        audioSources.mute = mute;       
        audioSources.clip = backgroundSound[backgroundAudioIndex];
        audioSources.Play();
        

        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.SetInt(BackgroundAudioKey, backgroundAudioIndex);
        PlayerPrefs.SetInt(MuteKey, mute ? 1 : 0);
        PlayerPrefs.Save();
        backgroundVolume = volume;
        isMuted = mute;
    }

    public void LoadSettings(float volume, int backgroundAudioIndex, bool mute)
    {
        backgroundVolume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);
        num=PlayerPrefs.GetInt(BackgroundAudioKey, backgroundAudioIndex);
        isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
    }
  
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(VolumeKey, backgroundVolume);
        PlayerPrefs.SetInt(BackgroundAudioKey, num);
        PlayerPrefs.SetInt(MuteKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public void ButtonClickSound()
    {
        audioSources.PlayOneShot(buttonClickSound);
    }

}
