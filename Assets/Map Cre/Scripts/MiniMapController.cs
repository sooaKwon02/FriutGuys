using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private MiniMapBoundaries boundaries;

    [SerializeField]
    private Transform miniMap;

    [SerializeField]
    private GameObject miniMapIcons;

    private Transform[] worldTransforms;
    private RectTransform[] imageTransforms;

    private RectTransform MapTransform => transform as RectTransform;

    private void Start()
    {
        InitializeWorldTransforms();
        InitializeImageTransforms();
    }

    private void Update()
    {
        UpdateMiniMap();
    }

    private void RegisterMiniMapObjects()
    {
        GameObject miniMapIcon = Instantiate(miniMapIcons) as GameObject;
        miniMapIcon.transform.SetParent(miniMap);
    }

    private void InitializeWorldTransforms()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        worldTransforms = new Transform[playerObjects.Length];

        for (int i = 0; i < playerObjects.Length; i++)
        {
            worldTransforms[i] = playerObjects[i].transform;
            RegisterMiniMapObjects();
        }
    }

    private void InitializeImageTransforms()
    {
        GameObject[] imageObjects = GameObject.FindGameObjectsWithTag("MiniMap Icon");
        imageTransforms = new RectTransform[imageObjects.Length];

        Color[] colors = new Color[]
        {
            new Color(1.0f, 0.0f, 0.0f, 1.0f),
            new Color(0.0f, 1.0f, 0.0f, 1.0f),
            new Color(0.0f, 0.0f, 1.0f, 1.0f),
            new Color(1.0f, 1.0f, 0.0f, 1.0f),
            new Color(0.0f, 1.0f, 1.0f, 1.0f),
            new Color(1.0f, 0.0f, 1.0f, 1.0f),
            new Color(1.0f, 0.5f, 0.0f, 1.0f),
            new Color(0.5f, 1.0f, 0.0f, 1.0f),
            new Color(1.0f, 0.0f, 0.5f, 1.0f),
            new Color(0.5f, 0.0f, 1.0f, 1.0f),
            new Color(0.0f, 1.0f, 0.5f, 1.0f),
            new Color(0.0f, 0.5f, 1.0f, 1.0f),
            new Color(0.5f, 1.0f, 0.75f, 1.0f),
            new Color(0.75f, 1.0f, 0.5f, 1.0f),
            new Color(0.5f, 0.75f, 1.0f, 1.0f),
            new Color(0.75f, 0.5f, 1.0f, 1.0f)
        };

        ShuffleArray(colors);

        for (int i = 0; i < imageObjects.Length; i++)
        {
            imageTransforms[i] = (RectTransform)imageObjects[i].transform;

            if (imageTransforms[i] != null)
            {
                int colorIndex = i % colors.Length;
                imageTransforms[i].GetComponent<Image>().color = colors[colorIndex];
            }
        }
    }

    private void UpdateMiniMap()
    {
        for (int i = 0; i < worldTransforms.Length; i++)
        {
            if (worldTransforms[i] == null)
            {
                Nullified(i);
            }

            else
            {
                if (i >= imageTransforms.Length)
                {
                    break;
                }

                Vector2 position = FindInterfacePoint(worldTransforms[i].position);
                imageTransforms[i].anchoredPosition = position;
            }
        }
    }

    private Vector2 FindInterfacePoint(Vector3 worldPosition)
    {
        Vector2 normalizedPosition = boundaries.FindNormalizedPosition(worldPosition);
        return Rect.NormalizedToPoint(MapTransform.rect, normalizedPosition);
    }

    private void Nullified(int i)
    {
        worldTransforms[i] = null;
        imageTransforms[i].gameObject.SetActive(false);
    }

    private void ShuffleArray<T>(T[] array)
    {
        System.Random range = new System.Random();
        int arrayLength = array.Length;
        while (arrayLength > 1)
        {
            arrayLength--;
            int arrayRange = range.Next(arrayLength + 1);
            T value = array[arrayRange];
            array[arrayRange] = array[arrayLength];
            array[arrayLength] = value;
        }
    }
}