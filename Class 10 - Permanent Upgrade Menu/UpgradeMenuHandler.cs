using UnityEngine;

public class UpgradeMenuHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PermanentUpgradeMenu.instance.OpenPanel();
    }

    private void OnDestroy()
    {
        PermanentUpgradeMenu.instance.ClosePanel();
    }
}
