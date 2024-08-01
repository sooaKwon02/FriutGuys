using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class PLAYER
    {
        public string ID;
        public string nickName;

        public string body_name;
        public float body_x;
        public float body_y;
        public float body_z;
        public float body_rotX;
        public float body_rotY;
        public float body_rotZ;

        public string glove1_name;
        public float glove1_x;
        public float glove1_y;
        public float glove1_z;
        public float glove1_rotX;
        public float glove1_rotY;
        public float glove1_rotZ;

        public string glove2_name;
        public float glove2_x;
        public float glove2_y;
        public float glove2_z;
        public float glove2_rotX;
        public float glove2_rotY;
        public float glove2_rotZ;

        public string head_name;
        public float head_x;
        public float head_y;
        public float head_z;
        public float head_rotX;
        public float head_rotY;
        public float head_rotZ;

        public string tail_name;
        public float tail_x;
        public float tail_y;
        public float tail_z;
        public float tail_rotX;
        public float tail_rotY;
        public float tail_rotZ;

        public string item1;
        public string item2;
        public int gameMoney;
        public int cashMoney;
        public int score;
    }
    public PLAYER player;
   

    private string saveUrl = "http://61.99.10.173/fruitsGuys/PlayerItemSave.php";
    private string loadUrl = "http://61.99.10.173/fruitsGuys/PlayerItemLoad.php";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
   
    private void OnApplicationQuit()
    {
        OnDataChanged(player.ID); // 게임 종료 시 데이터 저장
    }
    // 데이터가 변경되면 호출되는 메소드
    public void OnDataChanged(string id)
    {
        StartCoroutine(SaveDataCoroutine(id));
        StartCoroutine(LoadDataCoroutine(id));
    }
    public void NickNameSet(string nick)
    {
        player.nickName = nick;
    }

    // 데이터를 서버에 저장하는 코루틴
    private IEnumerator SaveDataCoroutine(string id)
    {
        CharacterCustom c = FindObjectOfType<CharacterCustom>() ? FindObjectOfType<CharacterCustom>() : null;
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("body_name", c.body.GetComponent<MeshFilter>().mesh.name);
        form.AddField("body_x", c.body.transform.localScale.x.ToString());
        form.AddField("body_y", c.body.transform.localScale.y.ToString());
        form.AddField("body_z", c.body.transform.localScale.z.ToString());
        form.AddField("body_rotX", c.body.transform.localRotation.x.ToString());
        form.AddField("body_rotY", c.body.transform.localRotation.x.ToString());
        form.AddField("body_rotZ", c.body.transform.localRotation.z.ToString());

        form.AddField("glove1_name", c.glove1.GetComponent<MeshFilter>().mesh.name);
        form.AddField("glove1_x", c.glove1.transform.localScale.x.ToString());
        form.AddField("glove1_y", c.glove1.transform.localScale.y.ToString());
        form.AddField("glove1_z", c.glove1.transform.localScale.z.ToString());
        form.AddField("glove1_rotX", c.glove1.transform.localRotation.x.ToString());
        form.AddField("glove1_rotY", c.glove1.transform.localRotation.y.ToString());
        form.AddField("glove1_rotZ", c.glove1.transform.localRotation.z.ToString());

        form.AddField("glove2_name", c.glove2.GetComponent<MeshFilter>().mesh.name);
        form.AddField("glove2_x", c.glove2.transform.localScale.x.ToString());
        form.AddField("glove2_y", c.glove2.transform.localScale.y.ToString());
        form.AddField("glove2_z", c.glove2.transform.localScale.z.ToString());
        form.AddField("glove2_rotX", c.glove2.transform.localRotation.x.ToString());
        form.AddField("glove2_rotY", c.glove2.transform.localRotation.y.ToString());
        form.AddField("glove2_rotZ", c.glove2.transform.localRotation.z.ToString());

        form.AddField("head_name", c.head.GetComponent<MeshFilter>().mesh.name);
        form.AddField("head_x", c.head.transform.localScale.x.ToString());
        form.AddField("head_y", c.head.transform.localScale.y.ToString());
        form.AddField("head_z", c.head.transform.localScale.z.ToString());
        form.AddField("head_rotX", c.head.transform.localRotation.x.ToString());
        form.AddField("head_rotY", c.head.transform.localRotation.y.ToString());
        form.AddField("head_rotZ", c.head.transform.localRotation.z.ToString());

        form.AddField("tail_name", c.tail.GetComponent<MeshFilter>().mesh.name);
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
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Data saved successfully.");
            }
        }
    }






    //===========================================================================================================
    // 데이터를 서버에서 로드하는 메소드
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
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);

                if (data != null && string.IsNullOrEmpty(data.error))
                {
                    Debug.Log("Data loaded successfully.");
                    player.ID = data.id;

                    player.body_name = data.body_name;
                    player.body_x = data.body_x;
                    player.body_y = data.body_y;
                    player.body_z = data.body_z;
                    player.body_rotX = data.body_rotX;
                    player.body_rotY = data.body_rotY;
                    player.body_rotZ = data.body_rotZ;

                    player.glove1_name = data.glove1_name;
                    player.glove1_x = data.glove1_x;
                    player.glove1_y = data.glove1_y;
                    player.glove1_z = data.glove1_z;
                    player.glove1_rotX = data.glove1_rotX;
                    player.glove1_rotY = data.glove1_rotY;
                    player.glove1_rotZ = data.glove1_rotZ;

                    player.glove2_name = data.glove2_name;
                    player.glove2_x = data.glove2_x;
                    player.glove2_y = data.glove2_y;
                    player.glove2_z = data.glove2_z;
                    player.glove2_rotX = data.glove2_rotX;
                    player.glove2_rotY = data.glove2_rotY;
                    player.glove2_rotZ = data.glove2_rotZ;

                    player.head_name = data.head_name;
                    player.head_x = data.head_x;
                    player.head_y = data.head_y;
                    player.head_z = data.head_z;
                    player.head_rotX = data.head_rotX;
                    player.head_rotY = data.head_rotY;
                    player.head_rotZ = data.head_rotZ;

                    player.tail_name = data.tail_name;
                    player.tail_x = data.tail_x;
                    player.tail_y = data.tail_y;
                    player.tail_z = data.tail_z;
                    player.tail_rotX = data.tail_rotX;
                    player.tail_rotY = data.tail_rotY;
                    player.tail_rotZ = data.tail_rotZ;

                    player.item1 = data.item1;
                    player.item2 = data.item2;
                    player.gameMoney = data.gameMoney;
                    player.cashMoney = data.cashMoney;
                    player.score = data.score;
                    Debug.Log("불러오기성공");
                }
                else
                {
                    Debug.LogError("Error loading data: " + data.error);
                }
            }
        }
    }


}
