using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    [SerializeField] string nameOfBGM = "BGMusic1";
    public AudioMixer audioMixer;

    private void Awake()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    private void Update()
    {
        //Debug.Log(audioMixer);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null)
        {
            s.source.Play();
        }        
    }

    public void SetMasterVolume(float volume)
    {
        //Debug.Log(audioMixer);
        audioMixer.SetFloat("masterVolume", volume);
        //Debug.Log("volume = " + volume + "\n Master volume = ");
    }
}
