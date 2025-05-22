using UnityEngine;

public class VS_PHLevelEnder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onPlayerDied.AddListener(EndLevel);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onPlayerDied.RemoveListener(EndLevel);
    }
    // Update is called once per frame
    void EndLevel()
    {
        SceneChanger.instance.LoadMenuScene();
    }
}
