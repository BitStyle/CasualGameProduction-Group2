﻿using System;
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

    /*
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
    */

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Debug.Log(audioMixer);
    }

    public void Play(string name)
    {
        AudioClip audioClipToUse = Array.Find(audioClips, clip => clip.name == name);
        if(audioClipToUse != null)
        {
            //audioSource.clip = audioClipToUse;
            audioSource.PlayOneShot(audioClipToUse);
        }    
        
        
    }

    public void SetMasterVolume(float volume)
    {
        //Debug.Log(audioMixer);
        audioMixer.SetFloat("masterVolume", volume);
        //Debug.Log("volume = " + volume + "\n Master volume = ");
    }
}