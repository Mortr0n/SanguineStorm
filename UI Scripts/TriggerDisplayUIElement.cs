using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnterScreenDirection { FromLeft, FromRight, FromTop, FromBottom }

public class TriggeredDisplayUIElement : MonoBehaviour
{
    [SerializeField] EnterScreenDirection enterFrom = EnterScreenDirection.FromLeft;

    Vector3 originalPosition = Vector3.zero;

    float originalPositionXRatio = 0;
    float originalPositionYRatio = 0;

    Vector3 offScreenPosition = Vector3.zero;
    float lerpTimer = 0.0f;
    bool comingOnScreen = false;
    bool goingOffScreen = false;
    bool onScreen = false;
    float onScreenTimer = 0;
    [SerializeField] float timeToStayOnScreen = 5.0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitializeDisplayPositions();

        UIManager.instance.onWindowResizedEvent?.AddListener(CalculateUIReposition);
    }



    // Update is called once per frame
    void Update()
    {
        if (comingOnScreen) RunComeOnScreen();
        if (goingOffScreen) RunGoingOffScreen();
        if (onScreen) RunOnScreenTimer();
    }

    #region Coming on-screen
    public void TriggerComeOnScreen()
    {
        if (onScreen)
        {
            onScreenTimer = 0;
        }
        else
        {
            lerpTimer = 0;
            comingOnScreen = true;
            goingOffScreen = false;
        }

    }

    void RunComeOnScreen()
    {
        lerpTimer += Time.deltaTime * 2;
        if (lerpTimer >= 1)
        {
            GetComponent<RectTransform>().position = originalPosition;
            EndComeOnScreen();
        }
        else
        {
            GetComponent<RectTransform>().position = Vector3.Lerp(offScreenPosition, originalPosition, lerpTimer);
        }
    }

    void EndComeOnScreen()
    {
        comingOnScreen = false;
        onScreen = true;
        onScreenTimer = 0;
    }
    #endregion

    #region On-Screen Timer
    void RunOnScreenTimer()
    {
        onScreenTimer += Time.deltaTime;
        if (onScreenTimer > timeToStayOnScreen)
        {
            TriggerGoingOffScreen();
            onScreen = false;
        }
    }
    #endregion
    #region Going off-screen
    public void TriggerGoingOffScreen()
    {
        lerpTimer = 0;
        comingOnScreen = false;
        goingOffScreen = true;
        onScreen = false;
    }
    void RunGoingOffScreen()
    {
        lerpTimer += Time.deltaTime;
        if (lerpTimer >= 1)
        {
            GetComponent<RectTransform>().position = offScreenPosition;
            EndGoingOffScreen();
        }
        else
        {
            GetComponent<RectTransform>().position = Vector3.Lerp(originalPosition, offScreenPosition, lerpTimer);
        }
    }
    void EndGoingOffScreen()
    {
        goingOffScreen = false;
    }
    #endregion

    #region Screen Position Utilities
    Vector3 GetOffScreenPosition()
    {
        Vector3 offScreenPosition = Vector3.zero;

        switch (enterFrom)
        {
            case EnterScreenDirection.FromRight:
                offScreenPosition = new Vector3(Screen.width + (Screen.width - originalPosition.x), originalPosition.y, 0);
                break;
            case EnterScreenDirection.FromLeft:
                offScreenPosition = new Vector3(-originalPosition.x, originalPosition.y, 0);
                break;
            case EnterScreenDirection.FromBottom:
                offScreenPosition = new Vector3(originalPosition.x, -originalPosition.y, 0);
                break;
            case EnterScreenDirection.FromTop:
                offScreenPosition = new Vector3(originalPosition.x, Screen.height + (Screen.height - originalPosition.y), 0);
                break;
        }

        return offScreenPosition;
    }

    private void InitializeDisplayPositions()
    {
        // Save the starting position
        originalPosition = GetComponent<RectTransform>().position;
        // Set the ui-element off-screen
        offScreenPosition = GetOffScreenPosition();
        GetComponent<RectTransform>().position = offScreenPosition;

        SaveUIRepositionRatios();
    }

    void SaveUIRepositionRatios()
    {
        originalPositionXRatio = originalPosition.x / Screen.width;
        originalPositionYRatio = originalPosition.y / Screen.height;
    }
    void CalculateUIReposition()
    {
        originalPosition = new Vector2(Screen.width * originalPositionXRatio,
                                        Screen.height * originalPositionYRatio);

        offScreenPosition = GetOffScreenPosition();
        GetComponent<RectTransform>().position = offScreenPosition;

        TriggerComeOnScreen();
    }
    #endregion
}
