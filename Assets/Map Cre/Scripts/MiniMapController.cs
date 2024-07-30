using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public static MiniMapController Instance;

    [SerializeField]
    private Vector2 worldSize;

    [SerializeField]
    private RectTransform scrollViewRectTransform;

    [SerializeField]
    private RectTransform contentRectTransform;

    [SerializeField]
    private MiniMapIcon minimapIconPrefab;

    private Matrix4x4 transformationMatrix;

    private readonly float zoomSpeed = 0.1f;
    private readonly float maxZoom = 3.0f;
    private readonly float minZoom = 1.0f;

    Dictionary<MiniMapWorldObject, MiniMapIcon> miniMapWorldObjectsLookup = new Dictionary<MiniMapWorldObject, MiniMapIcon>();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CalculateTransformationMatrix();
    }

    private void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        ZoomMap(zoom);
        UpdateMiniMapIcons();
    }

    public void RegisterMinimapWorldObject(MiniMapWorldObject miniMapWorldObject)
    {
        var minimapIcon = Instantiate(minimapIconPrefab);
        minimapIcon.transform.SetParent(contentRectTransform);
        minimapIcon.transform.SetParent(contentRectTransform);
        minimapIcon.Image.sprite = miniMapWorldObject.MiniMapIcon;
        miniMapWorldObjectsLookup[miniMapWorldObject] = minimapIcon;
    }

    public void RemoveMinimapWorldObject(MiniMapWorldObject minimapWorldObject)
    {
        if (miniMapWorldObjectsLookup.TryGetValue(minimapWorldObject, out MiniMapIcon icon))
        {
            miniMapWorldObjectsLookup.Remove(minimapWorldObject);
            Destroy(icon.gameObject);
        }
    }
    
    private void ZoomMap(float zoom)
    {
        if (zoom == 0)
        {
            return;
        }

        float currentMapScale = contentRectTransform.localScale.x;
        float zoomAmount = (zoom > 0 ? zoomSpeed : -zoomSpeed) * currentMapScale;
        float newScale = currentMapScale + zoomAmount;
        float clampedScale = Mathf.Clamp(newScale, minZoom, maxZoom);
        contentRectTransform.localScale = Vector3.one * clampedScale;
    }

    private void UpdateMiniMapIcons()
    {
        float iconScale = 1 / contentRectTransform.transform.localScale.x;

        foreach (var kvp in miniMapWorldObjectsLookup)
        {
            var miniMapWorldObject = kvp.Key;
            var miniMapIcon = kvp.Value;
            var mapPosition = WorldPositionToMapPosition(miniMapWorldObject.transform.position);

            miniMapIcon.RectTransform.anchoredPosition = mapPosition;
            var rotation = miniMapWorldObject.transform.rotation.eulerAngles;
            miniMapIcon.IconRectTransform.localRotation = Quaternion.AngleAxis(-rotation.y, Vector3.forward);
            miniMapIcon.IconRectTransform.localScale = Vector3.one * iconScale;
        }
    }

    private Vector2 WorldPositionToMapPosition(Vector3 worldPos)
    {
        var pos = new Vector2(worldPos.x, worldPos.z);
        return transformationMatrix.MultiplyPoint3x4(pos);
    }

    private void CalculateTransformationMatrix()
    {
        var minimapSize = contentRectTransform.rect.size;
        var worldSize = new Vector2(this.worldSize.x, this.worldSize.y);

        Debug.Log(minimapSize);
        Debug.Log(worldSize);

        var translation = -minimapSize / 2.0f;
        var scaleRatio = minimapSize / worldSize;

        Debug.Log(translation);
        Debug.Log(scaleRatio);

        //translation.x = 0f;
        //translation.y = -400.0f;

        transformationMatrix = Matrix4x4.TRS(translation, Quaternion.identity, scaleRatio);

        Debug.Log(transformationMatrix);

        //  {scaleRatio.x,   0,              0,   translation.x},
        //  {  0,            scaleRatio.y,   0,   translation.y},
        //  {  0,            0,              1,               0},
        //  {  0,            0,              0,               1}
    }
}