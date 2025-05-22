using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltipContent;
    [SerializeField] float tooltipOffsetY = 0.6f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 tooltipPos = transform.position;
        tooltipPos.x += GetComponent<RectTransform>().rect.width * 0.5f;
        tooltipPos.y += GetComponent<RectTransform>().rect.height * tooltipOffsetY;
        TooltipManager.instance.ShowTooltip(tooltipContent, tooltipPos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.HideTooltip();
    }
}
