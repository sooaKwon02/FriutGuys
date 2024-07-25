using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.SceneManagement;


public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager _instance;
    public static DatabaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DatabaseManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("DatabaseManager");
                    _instance = singletonObject.AddComponent<DatabaseManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    string secretKey = "1q2w3e4r!@#$";
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(SignUpComplete!=null)
        {
            SignUpComplete.SetActive(false);
        }
    }
   
    IEnumerator Start()
    {
        string url = "http://192.168.35.229/fruitsGuys/LoginUnity.php"; // PHP 스크립트의 URL을 입력하세요

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

            // JSON 데이터 파싱
            JArray jsonArray = JArray.Parse(jsonString);

            // 각 JSON 객체에서 필드 추출
            foreach (JObject json in jsonArray)
            {
                int no = (int)json["no"];
                string id = (string)json["id"];
                string password = (string)json["password"];
                string nickname = (string)json["nickname"];

                Debug.Log($"no: {no}, id: {id}, password: {password}, nickname: {nickname}");
            }
        }
    }

    public InputField id;
    public InputField password;
    public InputField nickname;
    public GameObject SignUpComplete;
    public GameObject SignUpPanel;
    string idtext;
    string passwordtext;
    string nicknametext;
    //=============================================================회원가입==============
    public void SignUpButton()
    {
        foreach(char i in id.text)
        {
            if ((i >= 48 && i <= 57) || (i >= 65 && i <= 90) || (i >= 97 && i <= 122))
            {
                idtext += i;
            }
            else
            {
                idtext = null;
                break;
            }    
        }
        foreach (char i in password.text)
        {
            if ((i >= 48 && i <= 57) || (i >= 65 && i <= 90) || (i >= 97 && i <= 122))
            {
                passwordtext += i;
            }
            else
            {
                passwordtext = null;
                break;
            }
        }
        foreach (char i in nickname.text)
        {
            if ((i >= 48 && i <= 57) || (i >= 65 && i <= 90) || (i >= 97 && i <= 122))
            {
                nicknametext += i;
            }
            else
            {
                nicknametext = null;
                break;
            }
        }
        if (idtext != null&& passwordtext != null&& nicknametext != null)
            StartCoroutine(SignUp(id.text, password.text, nickname.text));
        else
        {
            SignUpComplete.SetActive(true);
            SignUpComplete.GetComponentInChildren<Text>().text = "제대로 입력하세요";
        }            

    }

    IEnumerator SignUp(string id, string password, string nickname)
    {
        string serverURL = "http://192.168.35.229/fruitsGuys/SignUp.php";
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

            // byte 배열을 16진수 문자열로 변환
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2")); // "x2"는 16진수로 출력하기 위해 사용
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
    IEnumerator LoginRequest(string id, string password)
    {
        string serverURL = "http://192.168.35.229/fruitsGuys/GameLogin.php";
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
                if(responseText== "로그인성공")
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
        if( num==1)
        {
            SignUpPanel.SetActive(false);
            SignUpComplete.SetActive(false);
        }
       
        SignUpComplete.GetComponentInChildren<Text>().text = null;
    }
}