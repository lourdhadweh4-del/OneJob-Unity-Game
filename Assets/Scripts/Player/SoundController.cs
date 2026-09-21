using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    public void PlayMusic()
    {
        audioManager.PlayMusic("Music");
    }

    public void StopMusic()
    {
        audioManager.StopMusic("Music");
    }

    public void PlayBtnClick()
    {
        audioManager.PlaySFX("ButtonClick");
    }

    public void StopBtnClick()
    {
        audioManager.StopSFX("ButtonClick");
    }
}
