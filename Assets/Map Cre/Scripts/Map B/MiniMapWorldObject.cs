using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapWorldObject : MonoBehaviour
{
    [SerializeField]
    private bool followObject = false;

    [SerializeField]
    private Sprite minimapIcon;

    public Sprite MiniMapIcon => minimapIcon;

    private void Start()
    {
        MiniMapController.Instance.RegisterMinimapWorldObject(this, followObject);
    }

    private void OnDestroy()
    {
        MiniMapController.Instance.RemoveMinimapWorldObject(this);
    }
}