using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public GameObject tooltip; // 툴팁 UI 오브젝트
    public Text tooltipText; // 툴팁 텍스트

    void Start()
    { 
        foreach(ToolTipTrigger tip in FindObjectsOfType<ToolTipTrigger>())
        {
            tip.tooltip=this;
        }
        tooltip.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }  
    public void HideTooltip()
    {
        tooltip.SetActive(false); // 툴팁 비활성화
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
        rt.sizeDelta = screenSize / 8;
        rt.position = position;
        
    }
}
