using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;
    public string ID;
    public string nickName;
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

    [System.Serializable]
    public class ScoreEntry
    {
        public string nickname;
        public int score;
    }

    [System.Serializable]
    public class ScoreList
    {
        public ScoreEntry[] entries;
    }

    public ScoreList rankList;

    Item item;
    public PLAYER player;
    public INVENTYPE inventype = null;
    public Inventory inventory;
    public int[] useNum = new int[0];
    public string[] useName = new string[0];
    public int[] fashionNum = new int[0];
    public string[] fashionName = new string[0];

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }  
    private void OnApplicationQuit()
    {
        SaveData();
        if(inventory != null)
        {
            SaveInven();
        }
        StartCoroutine(UpdateIsActiveStatus(1));
    }
    public IEnumerator UpdateIsActiveStatus(int isActive)
    {
        //string url= "http://61.99.10.173/fruitsGuys/IsActive.php";
        string url = "http://192.168.35.229/fruitsGuys/IsActive.php";
        WWWForm form = new WWWForm();
        form.AddField("id", ID);
        form.AddField("isActive", isActive);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
        }
    }
    public void SetGame(string _id, string _nick)
    {
        ID = _id;
        nickName = _nick;
        StartCoroutine(UpdateIsActiveStatus(0));
    }

    public void SaveData()
    {
        StartCoroutine(SaveDataCoroutine());
    }
    private IEnumerator SaveDataCoroutine()
    {
        //string url = "http://61.99.10.173/fruitsGuys/PlayerItemSave.php";
        string url = "http://192.168.35.229/fruitsGuys/PlayerItemSave.php";
        WWWForm form = new WWWForm();
        form.AddField("id", ID);

        form.AddField("body_name", player.body_name);
        form.AddField("body_x", player.body_x.ToString("F2"));
        form.AddField("body_y", player.body_y.ToString("F2"));
        form.AddField("body_z", player.body_z.ToString("F2"));
        form.AddField("body_rotX", player.body_rotX.ToString("000"));
        form.AddField("body_rotY", player.body_rotY.ToString("000"));
        form.AddField("body_rotZ", player.body_rotZ.ToString("000"));

        form.AddField("glove1_name", player.glove1_name);
        form.AddField("glove1_x", player.glove1_x.ToString("F2"));
        form.AddField("glove1_y", player.glove1_y.ToString("F2"));
        form.AddField("glove1_z", player.glove1_z.ToString("F2"));
        form.AddField("glove1_rotX", player.glove1_rotX.ToString("000"));
        form.AddField("glove1_rotY", player.glove1_rotY.ToString("000"));
        form.AddField("glove1_rotZ", player.glove1_rotZ.ToString("000"));

        form.AddField("glove2_name", player.glove2_name);
        form.AddField("glove2_x", player.glove2_x.ToString("F2"));
        form.AddField("glove2_y", player.glove2_y.ToString("F2"));
        form.AddField("glove2_z", player.glove2_z.ToString("F2"));
        form.AddField("glove2_rotX", player.glove2_rotX.ToString("000"));
        form.AddField("glove2_rotY", player.glove2_rotY.ToString("000"));
        form.AddField("glove2_rotZ", player.glove2_rotZ.ToString("000"));

        form.AddField("head_name", player.head_name);
        form.AddField("head_x", player.head_x.ToString("F2"));
        form.AddField("head_y", player.head_y.ToString("F2"));
        form.AddField("head_z", player.head_z.ToString("F2"));
        form.AddField("head_rotX", player.head_rotX.ToString("000"));
        form.AddField("head_rotY", player.head_rotY.ToString("000"));
        form.AddField("head_rotZ", player.head_rotZ.ToString("000"));

        form.AddField("tail_name", player.tail_name);
        form.AddField("tail_x", player.tail_x.ToString("F2"));
        form.AddField("tail_y", player.tail_y.ToString("F2"));
        form.AddField("tail_z", player.tail_z.ToString("F2"));
        form.AddField("tail_rotX", player.tail_rotX.ToString("000"));
        form.AddField("tail_rotY", player.tail_rotY.ToString("000"));
        form.AddField("tail_rotZ", player.tail_rotZ.ToString("000"));

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
        //string url = "http://61.99.10.173/fruitsGuys/PlayerItemLoad.php";
        string url = "http://192.168.35.229/fruitsGuys/PlayerItemLoad.php";
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
        inventype.fashionInven = new INVEN[inventory.fashionItem.transform.childCount];
        inventype.useInven = new INVEN[inventory.useItem.transform.childCount];
        //string url = "http://61.99.10.173/fruitsGuys/PlayerInvenSave.php";
        string url = "http://192.168.35.229/fruitsGuys/PlayerInvenSave.php";
        for (int i = 0; i < inventory.fashionItem.transform.childCount; i++)
        {
            if (inventory.fashionItem.transform.GetChild(i).GetComponentInChildren<ItemData>().item != null && inventory.fashionItem.transform.childCount > 0)
            {
                item = inventory.fashionItem.transform.GetChild(i).GetComponentInChildren<ItemData>().item;
                inventype.fashionInven[i] = new INVEN { num = i, name = item.name };
            }
            else
                inventype.fashionInven[i] = new INVEN { num = i, name = "" };

        }
        for (int i = 0; i < inventory.useItem.transform.childCount; i++)
        {
            if (inventory.useItem.transform.GetChild(i).GetComponentInChildren<ItemData>().item != null && inventory.useItem.transform.childCount > 0)
            {
                item = inventory.useItem.transform.GetChild(i).GetComponentInChildren<ItemData>().item;
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
    private IEnumerator InvenLoadCoroutine()
    {
       // string url = "http://61.99.10.173/fruitsGuys/PlayerInvenLoad.php";
        string url = "http://192.168.35.229/fruitsGuys/PlayerInvenLoad.php";
        using (UnityWebRequest www = UnityWebRequest.Get($"{url}?id={UnityWebRequest.EscapeURL(ID)}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                ProcessResponse(www.downloadHandler.text);
            }
        }
    }
    private void ProcessResponse(string responseText)
    {
        string[] lines = responseText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        bool isUse = false;
        bool isFashion = false;

        List<int> use_num = new List<int>();
        List<string> use_name = new List<string>();
        List<int> fashion_num = new List<int>();
        List<string> fashion_name = new List<string>();


        foreach (string line in lines)
        {
            if (line.StartsWith("Use Inventory:"))
            {
                isUse = true;
                isFashion = false;
                continue;
            }
            if (line.StartsWith("Fashion Inventory:"))
            {
                isUse = false;
                isFashion = true;
                continue;
            }
            if (isUse || isFashion)
            {
                string cleanLine = line.Trim('[', ']', '"');
                var parts = cleanLine.Split(new[] { "],[" }, StringSplitOptions.None);

                if (parts.Length == 2)
                {
                    string[] numberStrings = parts[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var numStr in numberStrings)
                    {
                        if (int.TryParse(numStr.Trim(), out int itemNum))
                        {
                            if (isUse)
                            {
                                use_num.Add(itemNum);
                            }
                            else if (isFashion)
                            {
                                fashion_num.Add(itemNum);
                            }
                        }
                    }
                    string[] nameStrings = parts[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var nameStr in nameStrings)
                    {
                        var itemName = nameStr.Trim().Trim('"');
                        if (isUse)
                        {
                            use_name.Add(itemName);
                        }
                        else if (isFashion)
                        {
                            fashion_name.Add(itemName);
                        }
                    }
                }
            }

            useNum = use_num.ToArray();
            useName = use_name.ToArray();
            fashionNum = fashion_num.ToArray();
            fashionName = fashion_name.ToArray(); 

            inventype = new INVENTYPE
            {
                useInven = new INVEN[useNum.Length],
                fashionInven = new INVEN[fashionNum.Length]
            };

            for (int i = 0; i < useNum.Length; i++)
            {
                inventype.useInven[i] = new INVEN { num = useNum[i], name = useName[i] };
            }

            for (int i = 0; i < fashionNum.Length; i++)
            {
                inventype.fashionInven[i] = new INVEN { num = fashionNum[i], name = fashionName[i] };
            }
        }
    }
    public void LoadScore()
    {
        StartCoroutine(LoadTotalScore());
    }

    IEnumerator LoadTotalScore()
    {
        //string url = "http://61.99.10.173/fruitsGuys/TotalScore.php";
        string url = "http://192.168.35.229/fruitsGuys/TotalScore.php";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                ScoreList scoreList = JsonUtility.FromJson<ScoreList>(www.downloadHandler.text);
                rankList.entries = scoreList.entries;
            }
        }
    }
   
    IEnumerator UseItemSet(string _item1, string _item2)
    {
        if(_item1 == null )
        {
            player.item1 = "";
        }
        else 
        {
            player.item1 = _item1.ToString(); 
        }
        if (_item2 == null)
        {
            player.item2 = "";
        }
        else
        {
            player.item2 = _item2.ToString();
        }
        yield return null;
    }
  
   
}
