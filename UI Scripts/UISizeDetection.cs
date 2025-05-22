using UnityEngine;

public class UISizeDetection : MonoBehaviour
{
    bool firstFramePassed = false;

    void Start()
    {
        firstFramePassed = true;
    }

    private void OnRectTransformDimensionsChange()
    {
        if (firstFramePassed) UIManager.instance.RunWindowResize();
    }

    void Update()
    {

    }
}