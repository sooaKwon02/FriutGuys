using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour, IPointerEnterHandler
{
    public GameObject tooltip; // ���� UI ������Ʈ
    public Text tooltipText; // ���� �ؽ�Ʈ
   
    void Start()
    { 
        foreach (Transform obj in GameObject.FindObjectsOfType<Transform>(true))
        {
            ToolTipTrigger tipTrigger = obj.GetComponent<ToolTipTrigger>();
            if (tipTrigger != null)
            {
                tipTrigger.tooltip = this;
            }
        }
        tooltip.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        HideTooltip();

    }
    public void HideTooltip()
    {
        tooltip.SetActive(false); // ���� ��Ȱ��ȭ
    }
    public void ShowTooltip(string message, Vector3 position)
    {
        tooltip.SetActive(true);
        tooltipText.text = message;
        RectTransform rt = tooltip.GetComponent<RectTransform>();
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 screenCenter = screenSize / 2;

        bool isCenterX = position.x > screenCenter.x;
        bool isCenterY = position.y > screenCenter.y;
        if (isCenterX && isCenterY)
        {
            rt.pivot = new Vector2(1.1f, 1.1f);
        }
        else if(!isCenterX && isCenterY)
        {
            rt.pivot = new Vector2(-0.1f, 1.1f);
        }
        else if (isCenterX && !isCenterY)
        {
            rt.pivot = new Vector2(1.1f, -0.1f);
        }
        else
        {
            rt.pivot = new Vector2(-0.1f, -0.1f);
        }
        rt.sizeDelta = screenSize / 10;
        rt.position = position;

    }
   
}