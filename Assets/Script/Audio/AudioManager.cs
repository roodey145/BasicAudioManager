using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public volatile static AudioManager audioManager;
    [SerializeField]
    private AudioSource _effectSource, _musicSource;

    private void Awake()
    {
        // Singleton pattern
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this, 0);
    }


    public void PlayEffect(AudioClip effectClip)
    {
        _effectSource.PlayOneShot(effectClip);
    }

    public void AdjustVolume(float volume)
    {
        if (volume < 0)
            volume = 0;
        if (volume > 1)
            volume = 1;
        AudioListener.volume = volume;
    }

}
