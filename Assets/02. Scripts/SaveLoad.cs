using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System;
using static UnityEngine.Networking.UnityWebRequest;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class PLAYER
    {
        
        public string body_name;
        public float body_x, body_y, body_z;
        public float body_rotX, body_rotY, body_rotZ;
        public string glove1_name;
        public float glove1_x, glove1_y, glove1_z;
        public float glove1_rotX, glove1_rotY, glove1_rotZ;
        public string glove2_name;
        public float glove2_x, glove2_y, glove2_z;
        public float glove2_rotX, glove2_rotY, glove2_rotZ;
        public string head_name;
        public float head_x, head_y, head_z;
        public float head_rotX, head_rotY, head_rotZ;
        public string tail_name;
        public float tail_x, tail_y, tail_z;
        public float tail_rotX, tail_rotY, tail_rotZ;
        public string item1, item2;
        public int gameMoney, cashMoney, score;
    }

    [System.Serializable]
    public class INVEN
    {
        public int num;
        public string name;
    }

    [System.Serializable]
    public class INVENTYPE
    {
        public INVEN[] useInven;
        public INVEN[] fashionInven;
    }
    public string ID;
    public string nickName;
    public PLAYER player;
    public INVENTYPE inventype;
    public Inventory inventory;
    public INVEN[] use;
    public INVEN[] fashion;
  
    Item item;
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
       
        
    }
    public void InventorySet()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnApplicationQuit()
    {
        SaveData();
        SaveInven();
    }

    public void SetNickName(string _id ,string _nick)
    {
        ID = _id;
        nickName = _nick;
    }

    public void SaveData()
    {
        StartCoroutine(SaveDataCoroutine());
    }
    private IEnumerator SaveDataCoroutine()
    {
        CharacterCustom c = FindObjectOfType<CharacterCustom>();
        string url = "http://61.99.10.173/fruitsGuys/PlayerItemSave.php";
        if (c == null)
        {
            Debug.LogError("CharacterCustom not found!");
            yield break;
        }
        WWWForm form = new WWWForm();
        form.AddField("id",ID);

        form.AddField("body_name",player.body_name);
        form.AddField("body_x", c.body.transform.localScale.x.ToString());
        form.AddField("body_y", c.body.transform.localScale.y.ToString());
        form.AddField("body_z", c.body.transform.localScale.z.ToString());
        form.AddField("body_rotX", c.body.transform.localRotation.x.ToString());
        form.AddField("body_rotY", c.body.transform.localRotation.y.ToString());
        form.AddField("body_rotZ", c.body.transform.localRotation.z.ToString());

        form.AddField("glove1_name", player.glove1_name);
        form.AddField("glove1_x", c.glove1.transform.localScale.x.ToString());
        form.AddField("glove1_y", c.glove1.transform.localScale.y.ToString());
        form.AddField("glove1_z", c.glove1.transform.localScale.z.ToString());
        form.AddField("glove1_rotX", c.glove1.transform.localRotation.x.ToString());
        form.AddField("glove1_rotY", c.glove1.transform.localRotation.y.ToString());
        form.AddField("glove1_rotZ", c.glove1.transform.localRotation.z.ToString());

        form.AddField("glove2_name", player.glove2_name);
        form.AddField("glove2_x", c.glove2.transform.localScale.x.ToString());
        form.AddField("glove2_y", c.glove2.transform.localScale.y.ToString());
        form.AddField("glove2_z", c.glove2.transform.localScale.z.ToString());
        form.AddField("glove2_rotX", c.glove2.transform.localRotation.x.ToString());
        form.AddField("glove2_rotY", c.glove2.transform.localRotation.y.ToString());
        form.AddField("glove2_rotZ", c.glove2.transform.localRotation.z.ToString());

        form.AddField("head_name", player.head_name);
        form.AddField("head_x", c.head.transform.localScale.x.ToString());
        form.AddField("head_y", c.head.transform.localScale.y.ToString());
        form.AddField("head_z", c.head.transform.localScale.z.ToString());
        form.AddField("head_rotX", c.head.transform.localRotation.x.ToString());
        form.AddField("head_rotY", c.head.transform.localRotation.y.ToString());
        form.AddField("head_rotZ", c.head.transform.localRotation.z.ToString());

        form.AddField("tail_name", player.tail_name);
        form.AddField("tail_x", c.tail.transform.localScale.x.ToString());
        form.AddField("tail_y", c.tail.transform.localScale.y.ToString());
        form.AddField("tail_z", c.tail.transform.localScale.z.ToString());
        form.AddField("tail_rotX", c.tail.transform.localRotation.x.ToString());
        form.AddField("tail_rotY", c.tail.transform.localRotation.y.ToString());
        form.AddField("tail_rotZ", c.tail.transform.localRotation.z.ToString());

        form.AddField("item1", player.item1);
        form.AddField("item2", player.item2);
        form.AddField("gameMoney", player.gameMoney.ToString());
        form.AddField("cashMoney", player.cashMoney.ToString());
        form.AddField("score", player.score.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();         
        }
    }
    public void LoadData()
    {
        StartCoroutine(LoadDataCoroutine());
    }
    private IEnumerator LoadDataCoroutine()
    {
        string url = "http://61.99.10.173/fruitsGuys/PlayerItemLoad.php";
        WWWForm form = new WWWForm();
        form.AddField("id", ID);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            string json = www.downloadHandler.text;
            PLAYER data = JsonUtility.FromJson<PLAYER>(json);
            player = data;
        }
    }
    public void SaveInven()
    {
        StartCoroutine(InvenSaveCoroutine());
    }
    public IEnumerator InvenSaveCoroutine()
    {
        inventype.fashionInven = new INVEN[inventory.fashionInventory.transform.childCount];
        inventype.useInven = new INVEN[inventory.useInventory.transform.childCount];
        string url = "http://61.99.10.173/fruitsGuys/PlayerInvenSave.php";
        for (int i = 0; i < inventory.fashionInventory.transform.childCount; i++)
        {
            if (inventory.fashionInventory.transform.GetChild(i).GetComponentInChildren<ItemData>().item != null && inventory.fashionInventory.transform.childCount > 0)
            {                
                item = inventory.fashionInventory.transform.GetChild(i).GetComponentInChildren<ItemData>().item;
                inventype.fashionInven[i] = new INVEN { num = i, name = item.name };
            }    
            else
                inventype.fashionInven[i] = new INVEN { num = i, name = "" };
         
        }
        for (int i = 0; i < inventory.useInventory.transform.childCount; i++)
        {
            if (inventory.useInventory.transform.GetChild(i).GetComponentInChildren<ItemData>().item != null && inventory.useInventory.transform.childCount > 0)
            {
                item = inventory.useInventory.transform.GetChild(i).GetComponentInChildren<ItemData>().item;
                inventype.useInven[i] = new INVEN { num = i, name = item.name };
            }
            else
                inventype.useInven[i] = new INVEN { num = i, name = "" };
           
         
        }
        var data = new { id = ID, use = inventype.useInven, fashion = inventype.fashionInven, };
        string setitem = JsonConvert.SerializeObject(data);
        using (UnityWebRequest www = UnityWebRequest.Post(url, setitem))
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(setitem);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
        }
    }
    public void LoadInven()
    {
        StartCoroutine(InvenLoadCoroutine());
    }
    public IEnumerator InvenLoadCoroutine()
    {
        string url = "http://61.99.10.173/fruitsGuys/PlayerInvenLoad.php";
        string urlWithParams = $"{url}?id={UnityWebRequest.EscapeURL(ID)}";

        using (UnityWebRequest www = UnityWebRequest.Get(urlWithParams))
        {
            yield return www.SendWebRequest();

            Debug.Log("Server Response: " + www.downloadHandler.text);

            if (www.result == UnityWebRequest.Result.Success)
            {
                var responseText = www.downloadHandler.text;
                var data = JsonConvert.DeserializeObject<INVENTYPE>(responseText);
                Debug.Log(www.downloadHandler.text);
                //Debug.Log(data);
                //ProcessFetchedData(data);
            }           
        }    
    }
    //void ProcessFetchedData(INVENTYPE results)
    //{
    //    use = new INVEN[results.useInven.Length];
    //    fashion = new INVEN[results.fashionInven.Length];

    //    for (int i = 0; i < results.useInven.Length; i++)
    //    {
    //        var useInven = results.useInven[i];
    //        use[i] = new INVEN
    //        {
    //            num = ParseIntArray(useInven.num.ToString()),
    //            name = ParseStringArray(useInven.name.ToString())
    //        };
    //    }

    //    for (int i = 0; i < results.fashionInven.Length; i++)
    //    {
    //        var fashionInven = results.fashionInven[i];
    //        fashion[i] = new INVEN
    //        {
    //            num = ParseIntArray(fashionInven.num.ToString()),
    //            name = ParseStringArray(fashionInven.name.ToString())
    //        };
    //    }

    //    inventype = new INVENTYPE
    //    {
    //        useInven = use,
    //        fashionInven = fashion
    //    };
    //}
    private int[] ParseIntArray(string jsonArray)
    {
        if (string.IsNullOrEmpty(jsonArray))
        {
            return new int[0];
        }

        // 대괄호 제거 및 쉼표로 구분된 숫자 추출
        jsonArray = jsonArray.TrimStart('[').TrimEnd(']');
        var items = jsonArray.Split(','); 
        int[] result = new int[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            result[i] = int.Parse(items[i].Trim());
        }
        return items.Select(item => int.Parse(item.Trim())).ToArray();
    }

    private string[] ParseStringArray(string jsonArray)
    {
        if (string.IsNullOrEmpty(jsonArray))
        {
            return new string[0];
        }

        // 대괄호, 따옴표 제거 및 쉼표로 구분된 문자열 추출
        jsonArray = jsonArray.TrimStart('[').TrimEnd(']').Replace("\"", "");
        var items = jsonArray.Split(',');
        return items.Select(item => item.Trim()).ToArray();
    }
}
