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
    }
    IEnumerator Start()
    {
        string url = dll.LoginUnity; // PHP 스크립트의 URL을 입력하세요

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
                JArray jsonArray = JArray.Parse(jsonString); // JSON 데이터가 배열일 때

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
    //=============================================================회원가입==============
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
            SignUpComplete.GetComponentInChildren<Text>().text = "제대로 입력하세요";
        }

    }
    private bool IdPassword(string input)
    {
        string pattern = @"^[a-zA-Z0-9가-힣\p{P}\p{S}]*$";
        return Regex.IsMatch(input, pattern);
    }
    bool NickName(string input)
    {
        string pattern = @"^[a-zA-Z0-9가-힣]*$";
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
        // UnityWebRequest 생성 및 설정
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {

            yield return www.SendWebRequest(); // 요청 보내기

            if (www.result != UnityWebRequest.Result.Success)
            {
                SignUpComplete.SetActive(true);
                SignUpComplete.GetComponentInChildren<Text>().text = "서버에 연결할 수 없습니다";
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

            // byte 배열을 16진수 문자열로 변환
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    //===============================================로그인=======================
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
        string serverURL = dll.GameLogin; // 서버 URL을 설정
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                SignUpComplete.SetActive(true);
                SignUpComplete.GetComponentInChildren<Text>().text = "네트워크 오류";
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response Text: " + responseText);

                try
                {
                    LoginResponse response = JsonUtility.FromJson<LoginResponse>(responseText);

                    if (response.success)
                    {
                        //SaveLoadInven.Instance.LoadData(id);
                        //SaveLoad.Instance.LoadData(id);
                        //SaveLoad.Instance.player.ID = id;
                        //SaveLoad.Instance.NickNameSet(response.nickname); // 닉네임을 전달

                        SignUpComplete.SetActive(true);
                        SignUpComplete.GetComponentInChildren<Text>().text = "로그인 성공";
                    }
                    else
                    {
                        SignUpComplete.SetActive(true);
                        SignUpComplete.GetComponentInChildren<Text>().text = "로그인 실패: " + response.error;
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error parsing JSON: " + ex.Message);
                    SignUpComplete.SetActive(true);
                    SignUpComplete.GetComponentInChildren<Text>().text = "응답 파싱 오류";
                }
            }
        }
    }
    public void LoginMain(int num)
    {
        if (SignUpComplete.GetComponentInChildren<Text>().text == "회원가입 성공")
        {
            SignUpPanel.SetActive(false);
            SignUpComplete.SetActive(false);
        }
        else if (SignUpComplete.GetComponentInChildren<Text>().text == "로그인 성공")
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