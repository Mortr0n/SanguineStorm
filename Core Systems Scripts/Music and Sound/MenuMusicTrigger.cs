using UnityEngine;

public class MenuMusicTrigger : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMenuMusic();
    }
}
