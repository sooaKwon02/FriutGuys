using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using IPMINE;
using System.Text.RegularExpressions;

public class DatabaseManager : MonoBehaviour
{
    SaveLoad saveload;
    public static DatabaseManager Instance { get; private set; }
    IpMine dll = new IpMine();
    string secretKey = "1q2w3e4r!@#$";
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
        SignUpComplete.SetActive(false);
        saveload = FindObjectOfType<SaveLoad>();
    }
    IEnumerator Start()
    {
        string url = dll.LoginUnity; // PHP ��ũ��Ʈ�� URL�� �Է��ϼ���

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                Debug.Log("Received JSON: " + jsonString);
                JArray jsonArray = JArray.Parse(jsonString); // JSON �����Ͱ� �迭�� ��

                foreach (JObject json in jsonArray)
                {
                    string id = (string)json["id"];
                    string nickname = (string)json["nickname"];
                    Debug.Log($"ID: {id}, Nickname: {nickname}");
                }
            }
        }
    }

    public InputField id;
    public InputField password;
    public InputField nickname;
    public GameObject SignUpComplete;
    public GameObject SignUpPanel;
    [HideInInspector]
    public string idtext;
    [HideInInspector]
    public string passwordtext;
    [HideInInspector]
    public string nicknametext;
    //=============================================================ȸ������==============
    public void SignUpButton()
    {
        bool isIdValid = IdPassword(id.text);
        bool isPasswordValid = IdPassword(password.text);
        bool isNicknameValid = NickName(nickname.text);
        if (isIdValid && isPasswordValid && isNicknameValid)
            StartCoroutine(SignUp(id.text, password.text, nickname.text));
        else
        {
            SignUpComplete.SetActive(true);
            SignUpComplete.GetComponentInChildren<Text>().text = "����� �Է��ϼ���";
        }

    }
    private bool IdPassword(string input)
    {
        string pattern = @"^[a-zA-Z0-9��-�R\p{P}\p{S}]*$";
        return Regex.IsMatch(input, pattern);
    }
    bool NickName(string input)
    {
        string pattern = @"^[a-zA-Z0-9��-�R]*$";
        return Regex.IsMatch(input, pattern);
    }
    IEnumerator SignUp(string id, string password, string nickname)
    {
        string serverURL = dll.SignUp;
        string hash = CalculateSHA256Hash(id + password + nickname + secretKey);
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("password", password);
        form.AddField("nickname", nickname);
        form.AddField("hash", hash);
        // UnityWebRequest ���� �� ����
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {

            yield return www.SendWebRequest(); // ��û ������

            if (www.result != UnityWebRequest.Result.Success)
            {
                SignUpComplete.SetActive(true);
                SignUpComplete.GetComponentInChildren<Text>().text = "������ ������ �� �����ϴ�";
            }
            else
            {
                SignUpComplete.SetActive(true);
                SignUpComplete.GetComponentInChildren<Text>().text = www.downloadHandler.text.ToString();
            }
        }
    }
    private string CalculateSHA256Hash(string input)
    {
        using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // byte �迭�� 16���� ���ڿ��� ��ȯ
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    //===============================================�α���=======================
    public InputField loginIdInput;
    public InputField loginPasswordInput;
    public void GameLogin()
    {
        StartCoroutine(LoginRequest(loginIdInput.text.ToString(), loginPasswordInput.text.ToString()));
    }
    [System.Serializable]
    public class LoginResponse
    {
        public bool success;
        public string nickname;
        public string error;
    }
    IEnumerator LoginRequest(string id, string password)
    {
        string serverURL = dll.GameLogin; // ���� URL�� ����
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                SignUpComplete.SetActive(true);
                SignUpComplete.GetComponentInChildren<Text>().text = "��Ʈ��ũ ����";
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response Text: " + responseText);

                // JSON ������ LoginResponse ��ü�� ��ȯ
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(responseText);

                if (response.success)
                {
                    saveload.LoadData(id); // �г����� ����
                    saveload.SetNickName(id, response.nickname);

                    SignUpComplete.SetActive(true);
                    SignUpComplete.GetComponentInChildren<Text>().text = "�α��� ����";
                }
                else
                {
                    SignUpComplete.SetActive(true);
                    SignUpComplete.GetComponentInChildren<Text>().text = "�α��� ����: " + response.error;
                }
            }
        }
    }
    public void LoginMain(int num)
    {
        if (SignUpComplete.GetComponentInChildren<Text>().text == "ȸ������ ����")
        {
            SignUpPanel.SetActive(false);
            SignUpComplete.SetActive(false);
        }
        else if (SignUpComplete.GetComponentInChildren<Text>().text == "�α��� ����")
        {
            SceneManager.LoadScene(2);
        }
        else if (SignUpComplete.GetComponentInChildren<Text>().text != null)
        {
            SignUpPanel.SetActive(true);
            SignUpComplete.SetActive(false);
        }
        if (num == 1)
        {
            SignUpPanel.SetActive(false);
            SignUpComplete.SetActive(false);
        }

        SignUpComplete.GetComponentInChildren<Text>().text = null;
    }
}