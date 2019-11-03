using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRealmTransition : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 1f;
    bool inMortalRealm = true;
    AudioSource bgm;

    // Start is called before the first frame update
    void Awake()
    {
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
            StartCoroutine(SkyboxTransition(transitionSpeed));
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Transition")
        {
            bgm.Stop();
            if (!inMortalRealm)
            {
                bgm.GetComponent<BGM_Management>().SpiritRealmBGM();
                inMortalRealm = true;
            }
            else
            {
                bgm.Play();
                inMortalRealm = false;
            }
        }
    }

    private IEnumerator SkyboxTransition(float speed)
    {
        if (inMortalRealm)
        {
            for (float blend = RenderSettings.skybox.GetFloat("_Blend"); blend < 1f; blend += (Time.deltaTime / 2) * speed)
            {
                RenderSettings.skybox.SetFloat("_Blend", blend);
                yield return null;
            }
        }
        else
        {
            for (float blend = RenderSettings.skybox.GetFloat("_Blend"); blend > 0f; blend -= (Time.deltaTime / 2) * speed)
            {
                RenderSettings.skybox.SetFloat("_Blend", blend);
                yield return null;
            }
        }
    }
}
