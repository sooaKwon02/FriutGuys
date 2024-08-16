using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ToolTip tooltip;
    public string description;

    
    public void OnPointerEnter(PointerEventData eventData)
    {
       tooltip.ShowTooltip(description, Input.mousePosition);
       
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

}
