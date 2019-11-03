using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    [SerializeField] string[] audioClipNames;
    public AudioMixer audioMixer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string name)
    {
        AudioClip audioClipToUse = Array.Find(audioClips, clip => clip.name == name);
        if(audioClipToUse != null)
        {
            audioSource.PlayOneShot(audioClipToUse);
        }    
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }
}
