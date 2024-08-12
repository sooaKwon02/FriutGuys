using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwap : MonoBehaviour
{
    private void Start()
    {
        if (gameObject.GetComponent<PhotonView>() != null && gameObject.GetComponent<PhotonView>().IsMine)
        {
            Camera.main.transform.SetParent(gameObject.GetComponent<PlayerCtrl>().cam.transform);
            Camera.main.transform.localPosition = new Vector3(0, -0.71f, -6.15f);
        }
    }
}