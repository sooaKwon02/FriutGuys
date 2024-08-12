using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwap : MonoBehaviour
{
    private void Start()
    {
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int num = scene.buildIndex; 

        if (num == 3)
        {
            if (gameObject.GetComponent<PhotonView>().IsMine)
            {
                Transform cameraTransform = Camera.main.transform;
                Transform playerCamTransform = gameObject.GetComponent<PlayerCtrl>().cam.transform;

                cameraTransform.SetParent(playerCamTransform);
                cameraTransform.localPosition = new Vector3(0, -0.71f, -6.15f);
            }
        }
        else if (num == 10)
        {
            Camera cam = GetComponentInChildren<Camera>();
            if (cam)
            {
                cam.enabled = false;
            }
        }
        else
        {
            Camera cam = GetComponentInChildren<Camera>();
            if (cam)
            {
                cam.enabled = true;
            }
        }
    }
}
