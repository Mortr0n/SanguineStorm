using UnityEngine;
using TMPro;
using System.Collections;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    [SerializeField] private GameObject tooltipObject;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Vector2 offset = new Vector3(0, 50);

    private Coroutine hideCoroutine;
    private bool tooltipVisible;

    void Awake()
    {
        if (instance == null) instance = this;
        tooltipObject.SetActive(false);
    }

    public void ShowTooltip(string text, Vector3 position)
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        tooltipObject.transform.position = position + (Vector3)offset;
        tooltipText.text = text;

        if (!tooltipVisible)
        {
            tooltipObject.SetActive(true);
            tooltipVisible = true;
        }
    }

    public void HideTooltip(float delay = 0.2f)
    {
        if (hideCoroutine != null) return;
        hideCoroutine = StartCoroutine(HideAfterDelay(delay));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tooltipObject.SetActive(false);
        tooltipVisible = false;
        hideCoroutine = null;
    }
}
