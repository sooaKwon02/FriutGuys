using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public static MiniMapController Instance;

    [Header("References")]
    [SerializeField]
    private MiniMapBoundaries boundaries;

    [SerializeField]
    private Transform miniMap;

    [SerializeField]
    private GameObject miniMapIcons;

    [Header("World Player & MiniMap Player")]
    [SerializeField]
    private Transform[] worldTransforms;

    [SerializeField]
    private RectTransform[] imageTransforms;

    private RectTransform MapTransform => transform as RectTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeWorldTransforms();
        InitializeImageTransforms();
    }

    private void Update()
    {
        UpdateMiniMap();
    }

    private void RegisterMiniMapObject()
    {
        GameObject miniMapIcon = Instantiate(miniMapIcons);
        miniMapIcon.transform.SetParent(miniMap);
    }

    private void InitializeWorldTransforms()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        worldTransforms = new Transform[playerObjects.Length];
        for (int i = 0; i < playerObjects.Length; i++)
        {
            worldTransforms[i] = playerObjects[i].transform;
            RegisterMiniMapObject();
        }
    }

    private void InitializeImageTransforms()
    {
        GameObject[] imageObjects = GameObject.FindGameObjectsWithTag("MiniMap Icon");
        imageTransforms = new RectTransform[imageObjects.Length];
        for (int i = 0; i < imageObjects.Length; i++)
        {
            imageTransforms[i] = (RectTransform)imageObjects[i].transform;
        }
    }

    private void UpdateMiniMap()
    {
        if (worldTransforms.Length == 0 || imageTransforms.Length == 0) return;

        for (int i = 0; i < worldTransforms.Length; i++)
        {
            if (i >= imageTransforms.Length) break;

            Vector2 position = FindInterfacePoint(worldTransforms[i].position);
            imageTransforms[i].anchoredPosition = position;
        }
    }

    private Vector2 FindInterfacePoint(Vector3 worldPosition)
    {
        Vector2 normalizedPosition = boundaries.FindNormalizedPosition(worldPosition);
        return Rect.NormalizedToPoint(MapTransform.rect, normalizedPosition);
    }
}