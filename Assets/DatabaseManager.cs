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
    IpMine dll = new IpMine();
    string secretKey = "1q2w3e4r!@#$";
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SignUpComplete.SetActive(false);
    }
    IEnumerator Start()
    {
        string url = dll.LoginUnityMine; // PHP ½ºÅ©¸³Æ®ÀÇ URLÀ» ÀÔ·ÂÇÏ¼¼¿ä

        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string jsonString = www.text;
            Debug.Log("Received JSON: " + jsonString);

            // JSON µ¥ÀÌÅÍ ÆÄ½Ì
            JArray jsonArray = JArray.Parse(jsonString);

            // °¢ JSON °´Ã¼¿¡¼­ ÇÊµå ÃßÃâ
            foreach (JObject json in jsonArray)
            {
                string no = (string)json["no"];
                string id = (string)json["id"];
                string password = (string)json["password"];
                string nickname = (string)json["nickname"];

                Debug.Log($",no:{no} id: {id}, password: {password}, nickname: {nickname}");
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
            SignUpComplete.SetActive(true);
            SignUpComplete.GetComponentInChildren<Text>().text = "Á¦´ë·Î ÀÔ·ÂÇÏ¼¼¿ä";
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
        // UnityWebRequest »ý¼º ¹× ¼³Á¤
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            
            yield return www.SendWebRequest(); // ¿äÃ» º¸³»±â

            if (www.result != UnityWebRequest.Result.Success)
            {
                SignUpComplete.SetActive(true);
                SignUpComplete.GetComponentInChildren<Text>().text= "¼­¹ö¿¡ ¿¬°áÇÒ ¼ö ¾ø½À´Ï´Ù";
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

            // byte ¹è¿­À» 16Áø¼ö ¹®ÀÚ¿­·Î º¯È¯
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2")); // "x2"´Â 16Áø¼ö·Î Ãâ·ÂÇÏ±â À§ÇØ »ç¿ë
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
                SignUpComplete.GetComponentInChildren<Text>().text = "³×Æ®¿öÅ© ¿À·ù";
            }
            else
            {
                string responseText = www.downloadHandler.text;
                if(responseText== "·Î±×ÀÎ¼º°ø")
                {
                    SignUpComplete.SetActive(true);
                    SignUpComplete.GetComponentInChildren<Text>().text = responseText;
                }
                
                else
                {
                    SignUpComplete.SetActive(true);
                    SignUpComplete.GetComponentInChildren<Text>().text = responseText;
                }
                    
            }
        }
    }
    public void LoginMain(int num)
    {
        if (SignUpComplete.GetComponentInChildren<Text>().text == "È¸¿ø°¡ÀÔ ¼º°ø")
        {
            SignUpPanel.SetActive(false);
            SignUpComplete.SetActive(false);
        }
        else if (SignUpComplete.GetComponentInChildren<Text>().text == "·Î±×ÀÎ ¼º°ø")
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
       
        SignUpComplete.GetComponentInChildren<Text>().text = null;
    }
}