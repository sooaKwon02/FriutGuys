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
    Inventory inven;
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
        inven = FindObjectOfType<Inventory>();
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
        string serverURL = dll.SignUpMine;
        string hash = CalculateSHA256Hash(id + password + nickname + secretKey);
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("password", password);
        form.AddField("nickname", nickname);
        form.AddField("hash", hash);
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {            
            yield return www.SendWebRequest(); 
            if (www.result != UnityWebRequest.Result.Success)
            {
                SignUpComplete.SetActive(true);
                SignUpComplete.GetComponentInChildren<Text>().text= "서버에 연결할 수 없습니다";
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
        string serverURL = dll.GameLoginMine; 
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
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(responseText);

                if (response.success)
                {
                    saveload.SetNickName(id, response.nickname);
                    saveload.LoadData();
                    saveload.LoadInven();

                    SignUpComplete.SetActive(true);
                    SignUpComplete.GetComponentInChildren<Text>().text = "로그인 성공";
                }
                else
                {
                    SignUpComplete.SetActive(true);
                    SignUpComplete.GetComponentInChildren<Text>().text = "로그인 실패 ";
                }
            }
            LoginReset();
        }
        
    }
    public void LoginMain(int num)
    {
        if (SignUpComplete.GetComponentInChildren<Text>().text == "회원가입 성공"|| SignUpComplete.GetComponentInChildren<Text>().text == "로그인 실패")
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
        if( num==1)
        {
            SignUpPanel.SetActive(false);
            SignUpComplete.SetActive(false);
        }
        SignReset();
        SignUpComplete.GetComponentInChildren<Text>().text = null;
    }

    void SignReset()
    {
        id.text = null;
        password.text = null;
        nickname.text = null;
    }
    void LoginReset()
    {
        loginIdInput.text = null;
        loginPasswordInput.text = null;
    }
}