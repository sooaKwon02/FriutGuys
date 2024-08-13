using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    private Image image;
    private Material mat;

    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            mat = new Material(image.material);
            image.material = mat;

            mat.SetColor("_Color", normalColor);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mat != null)
        {
            mat.SetColor("_Color", highlightColor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mat != null)
        {
            mat.SetColor("_Color", normalColor);
        }
    }
}

