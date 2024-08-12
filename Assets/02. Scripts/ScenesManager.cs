using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    static public int SceneNum;
    public GameObject tooltipPrefab;
    public List<int> loadScenes = new List<int>();
    public int[] sceneIndex = new int[] { 5, 6, 7, 8, 9 };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneNum = 1;
        SceneManager.LoadScene(SceneNum);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

        PhotonNetwork.LoadLevel(selectScene);
    }
}
