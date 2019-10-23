using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sounds;

    [SerializeField] string nameOfBGM = "BGMusic1";
    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public Slider masterVolumeSlider;

    private void Awake()
    {
    }
    public void SetMasterVolume()
    {
        Debug.Log(audioMixer);
        //audioMixer.SetFloat("masterVolume", masterVolumeSlider.value);
    }

    private void Update()
    {
        float myValue;
        audioMixer.GetFloat("masterVolume", out myValue);
        Debug.Log(myValue);
    }
}
