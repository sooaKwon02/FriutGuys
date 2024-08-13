using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using IPMINE;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

public class DatabaseManager : MonoBehaviour
{
    SaveLoad saveload;
    Inventory inven;
    IpMine dll = new IpMine();
    string secretKey = "1q2w3e4r!@#$";

    public InputField id;
    public InputField password;
    public InputField nickname;
    public GameObject LoginPanel;
    public GameObject SignUpComplete;
    public GameObject SignUpPanel;
    public int isActive=1;
    Text resText;
    [HideInInspector]
    public string idtext;
    [HideInInspector]
    public string passwordtext;
    [HideInInspector]
    public string nicknametext;

    public InputField[] inputField;
    int ifndex = 0;

    private void Awake()
    {
        resText=SignUpComplete.GetComponentInChildren<Text>();
        saveload = FindObjectOfType<SaveLoad>();
        inven = FindObjectOfType<Inventory>();
    }
    private void Start()
    {
        LoginPanel.SetActive(true); 
        SignUpComplete.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MoveField();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameLogin();
        }   
    }

    void MoveField()
    {
        if (inputField.Length == 0)
            return;

        for (int i = 0; i < inputField.Length; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == inputField[i].gameObject)
            {
                ifndex = i;
                break;
            }
        }

        ifndex = (ifndex + 1) % inputField.Length;
        inputField[ifndex].Select();
    }
    //=============================================================È¸¿ø°¡ÀÔ==============
    public void SignUpButton()
    {
        bool isIdValid = IdPassword(id.text);
        bool isPasswordValid = IdPassword(password.text);
        bool isNicknameValid = NickName(nickname.text);
        if (isIdValid && isPasswordValid && isNicknameValid)
            StartCoroutine(SignUp(id.text, password.text, nickname.text));
        else
        {
           Text("Á¦´ë·Î ÀÔ·ÂÇÏ¼¼¿ä");
        }        
    }
    private bool IdPassword(string input)
    {
        string pattern = @"^[a-zA-Z0-9°¡-ÆR\p{P}\p{S}]*$";
        return Regex.IsMatch(input, pattern);
    }
    bool NickName(string input)
    {
        string pattern = @"^[a-zA-Z0-9°¡-ÆR]*$";
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
                Text("¼­¹ö¿¡ ¿¬°áÇÒ ¼ö ¾ø½À´Ï´Ù");
            }
            else
            {
                Text(www.downloadHandler.text.ToString());
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
    //===============================================·Î±×ÀÎ=======================
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
        public string result;
    }
    IEnumerator LoginRequest(string id, string password)
    {
        string serverURL = dll.GameLoginMine; 
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("password", password);
        form.AddField("isActive", 1);
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);
            if (response.success)
            {
                saveload.SetGame(id, response.nickname);
                saveload.LoadData();
                saveload.LoadInven();
                saveload.LoadScore();
                StartCoroutine(Success());
                LoginPanel.SetActive(false);
            }
            else
            {
                Text(response.result);
            }
            LoginReset();
        }
        
    }
    IEnumerator Success()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
    public void LoginMain(int num)
    {       
        if (num == 1)
        {
            SignUpPanel.SetActive(true);
        }        
        else
        {
            SignUpPanel.SetActive(false);
        }
        SignReset();
        resText.text = null;
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
    void Text(string str)
    {
        StartCoroutine(TextGet(str));
    }
    IEnumerator TextGet(string str)
    {
        SignUpComplete.SetActive(true);
        SignUpComplete.GetComponentInChildren<Text>().text = str;   
        yield return new WaitForSeconds(2f);
        SignUpComplete.GetComponentInChildren<Text>().text = null;
        SignUpComplete.SetActive(false);
    }
}