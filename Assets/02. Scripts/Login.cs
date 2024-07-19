using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public Text id;
    public Text password;
    public void SignUp()
    {

    }
    public void LoginGame()
    {
        SceneManager.LoadScene(2);
    }
}
