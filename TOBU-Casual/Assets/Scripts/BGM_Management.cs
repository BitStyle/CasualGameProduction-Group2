using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Management : MonoBehaviour
{
    [SerializeField] AudioClip mortalRealm;
    [SerializeField] AudioClip spiritRealm;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void MortalRealmBGM()
    {
        audioSource.clip = mortalRealm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SpiritRealmBGM()
    {
        audioSource.clip = spiritRealm;
        audioSource.loop = true;
        audioSource.Play();
    }

}
