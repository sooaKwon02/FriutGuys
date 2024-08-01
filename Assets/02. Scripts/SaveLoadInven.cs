using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SaveLoadInven : MonoBehaviour
{
    [System.Serializable]
    public class INVEN
    {
        public string item_name;
        public int item_quantity;
        public string item_type;
    }

    [System.Serializable]
    public class InventoryData
    {
        public string id;
        public List<INVEN> items;
    }

    public static SaveLoadInven Instance { get; private set; }
    private string saveUrl = "http://192.168.35.229/fruitsGuys/PlayerInvenSave.php";
    private string loadUrl = "http://192.168.35.229/fruitsGuys/PlayerInvenLoad.php";
    public INVEN[] item;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        OnDataChanged(FindObjectOfType<SaveLoad>().ID);
    }

    // �����Ͱ� ����Ǹ� ȣ��Ǵ� �޼ҵ�
    public void OnDataChanged(string id)
    {
        StartCoroutine(SaveDataCoroutine(id));
    }

    // �����͸� ������ �����ϴ� �ڷ�ƾ
    private IEnumerator SaveDataCoroutine(string _id)
    {
        InventoryData data = new InventoryData
        {
            id = _id,
            items = new List<INVEN>(item)
        };

        string json = JsonUtility.ToJson(data);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        using (UnityWebRequest www = new UnityWebRequest(saveUrl, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Data saved successfully. Response: " + www.downloadHandler.text);
            }
        }
    }

    // �����͸� �������� �ε��ϴ� �޼ҵ�
    public void LoadData(string id)
    {
        StartCoroutine(LoadDataCoroutine(id));
    }

    private IEnumerator LoadDataCoroutine(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        using (UnityWebRequest www = UnityWebRequest.Post(loadUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading data: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                Debug.Log("Received JSON: " + json);

                // InventoryData�� ��ȯ
                InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

                if (inventoryData != null && inventoryData.items != null)
                {
                    foreach (var item in inventoryData.items)
                    {
                        Debug.Log($"Item Name: {item.item_name}, Quantity: {item.item_quantity}, Type: {item.item_type}");
                    }
                }
                else
                {
                    Debug.LogError("No data found or error in loading.");
                }
            }
        }
    }
}
