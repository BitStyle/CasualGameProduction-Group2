using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Management : MonoBehaviour
{
    [SerializeField] AudioClip mortalRealm;
    [SerializeField] AudioClip spiritRealm;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void MortalRealmBGM()
    {
        audio.PlayOneShot(mortalRealm);
    }

    public void SpiritRealmBGM()
    {
        audio.PlayOneShot(spiritRealm);
    }

}
