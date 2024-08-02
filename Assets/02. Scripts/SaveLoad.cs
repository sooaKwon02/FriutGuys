using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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
    public string ID;
    public string nickName;
    public PLAYER player;


    private string saveUrl = "http://61.99.10.173/fruitsGuys/PlayerItemSave.php";
    private string loadUrl = "http://61.99.10.173/fruitsGuys/PlayerItemLoad.php";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        SaveData(ID); // 게임 종료 시 데이터 저장
    }

    public void SetNickName(string _id ,string _nick)
    {
        ID = _id;
        nickName = _nick;
    }

    public void SaveData(string id)
    {

        StartCoroutine(SaveDataCoroutine(id));
    }

    private IEnumerator SaveDataCoroutine(string id)
    {
        CharacterCustom c = FindObjectOfType<CharacterCustom>();
        if (c == null)
        {
            Debug.LogError("CharacterCustom not found!");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        // PlayerItem의 item 속성값을 직접 확인

        form.AddField("body_name",player.body_name);
        form.AddField("body_x", c.body.transform.localScale.x.ToString());
        form.AddField("body_y", c.body.transform.localScale.y.ToString());
        form.AddField("body_z", c.body.transform.localScale.z.ToString());
        form.AddField("body_rotX", c.body.transform.localRotation.x.ToString());
        form.AddField("body_rotY", c.body.transform.localRotation.y.ToString());
        form.AddField("body_rotZ", c.body.transform.localRotation.z.ToString());


            form.AddField("glove1_name",  player.glove1_name);
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

        using (UnityWebRequest www = UnityWebRequest.Post(saveUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error saving data: " + www.error);
            }
            else
            {
                Debug.Log("Data saved successfully.");
            }
        }
    }




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
                Debug.Log("Received JSON: " + json); // JSON 데이터 확인

                PLAYER data = JsonUtility.FromJson<PLAYER>(json);

                if (data != null)
                {
                    player = data;
                    Debug.Log("Data loaded successfully.");
                }
                else
                {
                    Debug.LogError("Error parsing loaded data.");
                }
            }
        }
    }

}
