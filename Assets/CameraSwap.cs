using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwap : MonoBehaviour
{
    private void OnEnable()
    {
        if (gameObject.GetComponent<PhotonView>() != null && gameObject.GetComponent<PhotonView>().IsMine)
        {
            Camera.main.transform.SetParent(gameObject.GetComponent<PlayerCtrl>().cam.transform);
            Camera.main.transform.localPosition = new Vector3(0, -0.71f, -6.15f);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int num = SceneManager.GetActiveScene().buildIndex;
        if(num!=10)
        {
            if (gameObject.GetComponent<PhotonView>() != null && gameObject.GetComponent<PhotonView>().IsMine)
            {
                GetComponentInChildren<Camera>().gameObject.SetActive(true);
                Camera.main.transform.SetParent(gameObject.GetComponent<PlayerCtrl>().cam.transform);
                Camera.main.transform.localPosition = new Vector3(0, -0.71f, -6.15f);
            }
        }
        else
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
      
    }
  
}

