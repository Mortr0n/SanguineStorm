using UnityEngine;

public class GameMusicTrigger : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayGameMusic();
    }
}
