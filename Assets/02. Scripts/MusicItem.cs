using UnityEngine;
using UnityEngine.UI;

public class MusicItem : MonoBehaviour
{
    Text musicName;
    Button button;
    int num;

    private void Awake()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(this.MusicPlay);
        musicName = GetComponentInChildren<Text>();
    }
    public void Setting(string _name,int _num)
    {
        musicName.text = _name;
        num = _num;
    }
   
    void MusicPlay()
    {
        AudioSource audioSource = FindObjectOfType<AudioSource>();
        MusicSet musicSet = GetComponentInParent<MusicSet>();
        audioSource.clip =SoundManager.Instance.backgroundSound[num];
        SoundManager.Instance.num= num;
        audioSource.Play();
        musicSet.musicName.text = audioSource.clip.name;
    }  
}
