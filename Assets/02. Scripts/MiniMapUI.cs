using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUI : MonoBehaviour
{
    [SerializeField]
    private Transform left;
    [SerializeField]
    private Transform right;
    [SerializeField]
    private Transform top;
    [SerializeField]
    private Transform bottom;

    [SerializeField]
    private Image minimapImage;
    [SerializeField]
    private Image minimapPlayerImage;

    public PlayerCtrl targetPlayer;

    void Start()
    {
        var inst = Instantiate(minimapImage.material);
        minimapImage.material = inst;

    }

    void Update()
    {
        if (targetPlayer != null)
        {
            //맵의 가로길이, 세로길이 계산
            Vector2 mapArea = new Vector2(Vector3.Distance(left.position, right.position), Vector3.Distance(bottom.position, top.position));
            //left와 bottom 사이의 플레이어 x,y 거리 계산
            Vector2 charPos = new Vector2(Vector3.Distance(left.position, new Vector3(targetPlayer.transform.position.x, 0f, 0f)),
                Vector3.Distance(bottom.position, new Vector3(0f, targetPlayer.transform.position.y, 0f)));
            //계산한 값들을 나눔
            Vector2 normalPos = new Vector2(charPos.x / mapArea.x, charPos.y / mapArea.y);

            minimapPlayerImage.rectTransform.anchoredPosition = new Vector2(minimapImage.rectTransform.sizeDelta.x * normalPos.x,
                minimapImage.rectTransform.sizeDelta.y * normalPos.y);
            Debug.Log(minimapImage.rectTransform.sizeDelta);
            Debug.Log("! : " + normalPos);
        }
    }
    //public RectTransform minimapRect;
    //public RectTransform playerIcon;
    //public Transform playerTransform;
    //public Vector2 minimapSize = new Vector2(200, 200);
    //public Vector2 worldSize = new Vector2(1000, 1000);

    //public List<Transform> objectsToTrack;
    //public List<RectTransform> objectIcons;

    //void Update()
    //{
    //    UpdatePlayerIconPosition();
    //    UpdateObjectIconsPosition();
    //}

    //void UpdatePlayerIconPosition()
    //{
    //    Vector3 playerPos = playerTransform.position;
    //    Vector2 minimapPos = new Vector2(
    //        (playerPos.x / worldSize.x) * minimapSize.x,
    //        (playerPos.z / worldSize.y) * minimapSize.y
    //    );

    //    playerIcon.anchoredPosition = minimapPos - minimapSize / 2;
    //}

    //void UpdateObjectIconsPosition()
    //{
    //    for (int i = 0; i < objectsToTrack.Count; i++)
    //    {
    //        Vector3 objPos = objectsToTrack[i].position;
    //        Vector2 minimapPos = new Vector2(
    //            (objPos.x / worldSize.x) * minimapSize.x,
    //            (objPos.z / worldSize.y) * minimapSize.y
    //        );

    //        objectIcons[i].anchoredPosition = minimapPos - minimapSize / 2;
    //    }
    //}
}
