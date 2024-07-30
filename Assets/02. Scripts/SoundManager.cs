using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip[] backgroundSound;
    public AudioClip buttonClickSound;
    public AudioClip playerMoveSound;

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
        audioSource=GetComponent<AudioSource>();
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
        audioSource.volume = volume;
        audioSource.clip = backgroundSound[backgroundAudioIndex];
        audioSource.mute = mute;
        audioSource.Play();

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

}
