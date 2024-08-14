using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    static public int SceneNum;
    public GameObject tooltipPrefab;
    public List<int> loadScenes = new List<int>();
    public int[] sceneIndex = new int[] { 6, 7, 8, 9 };
    public int count=-1;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneNum = 1;
        SceneManager.LoadScene(SceneNum);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
        XRDevice.deviceLoaded += OnDeviceLoaded;
    }
   
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        XRDevice.deviceLoaded -= OnDeviceLoaded;
    }
    private void OnDeviceLoaded(string deviceName)
    {
        Debug.Log("asd");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (Transform transform in FindObjectsOfType<Transform>(true))
        {
            Button button = transform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveListener(SoundManager.Instance.ButtonClickSound);
                button.onClick.AddListener(SoundManager.Instance.ButtonClickSound);
            }
        }

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas != null)
        {
            GameObject tooltipInstance = Instantiate(tooltipPrefab, canvas.transform);
            tooltipInstance.transform.SetAsLastSibling();
        }
        if(SceneManager.GetActiveScene().buildIndex!=0|| SceneManager.GetActiveScene().buildIndex!=1)
        SaveLoad.instance.SaveData();
    }
    public void LoadRandomScene()
    {
        List<int> sceneList = new List<int>(sceneIndex);
        
        foreach (int index in loadScenes)
        {
            sceneList.Remove(index);
        }

        if (sceneList.Count == 0)
        {
            loadScenes.Clear();
            sceneList = new List<int>(sceneIndex);
        }

        int randomIndex = Random.Range(0, sceneList.Count);
        int selectScene = sceneList[randomIndex];

        loadScenes.Add(selectScene);
        count++;
        PhotonNetwork.LoadLevel(selectScene);
    }
    IEnumerator ScoreMoneySet()
    {
        SaveLoad.instance.player.gameMoney += 100 * Add(count);
        SaveLoad.instance.player.score += 100 * Add(count);
        yield return new WaitForSeconds(1);
    }
    public void ScoreSet()
    {
        StartCoroutine(ScoreMoneySet());
    }
    int Add(int num)
    {
        int sum = 1;
        if (num == 0)
        {
            return sum;
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                sum *= 2;
            }
            return sum;
        }
    }
}
