using UnityEngine;

public class RoundManager : MonoBehaviour
{
    bool paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TogglePause();
    }

    public void TogglePause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            paused = true;
            Time.timeScale = 0.0f;
        }
    }
}
