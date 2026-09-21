using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        foreach(var sound in sounds)
        {
            sound.audioSource = gameObject.GetComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.playOnAwake = sound.playOnAwake;
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null)
        {
            Debug.Log("Sound: " + name + " is not found.");
            return;
        }

        sound.audioSource.Play();
    }

    public void StopMusic(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null)
        {
            Debug.Log("Sound: " + name + " is not found.");
            return;
        }

        sound.audioSource.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if(sound == null)
        {
            Debug.Log("Sound: " + name + " is not found.");
            return;
        }

        sound.audioSource.Play();
    }

    public void StopSFX(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null)
        {
            Debug.Log("Sound: " + name + " is not found.");
            return;
        }

        sound.audioSource.Stop();
    }
    

}
