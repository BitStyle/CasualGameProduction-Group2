using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SpiritRealmTransition : MonoBehaviour
{
    [SerializeField] PostProcessProfile postProcess;
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
            //StartCoroutine(SkyboxTransition(transitionSpeed));
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Transition")
        {
            WorldTransition();
        }
    }

    public void WorldTransition()
    {
        bgm.Stop();
        if (!GameManager.Instance.InSpiritWorld)
        {
            bgm.GetComponent<BGM_Management>().SpiritRealmBGM();
            //postProcess.GetSetting<ColorGrading>().hueShift.value = 180f;
            StartCoroutine(ColorTransition(180.0f, 0.0f, 0.5f));
            GameManager.Instance.InSpiritWorld = true;
        }
        else
        {
            bgm.GetComponent<BGM_Management>().MortalRealmBGM();
            //postProcess.GetSetting<ColorGrading>().hueShift.value = 0f;
            StartCoroutine(ColorTransition(0.0f, 180.0f, 1.0f));
            GameManager.Instance.InSpiritWorld = false;
        }
    }

    public void SetDefault()
    {
        bgm.Play();
        postProcess.GetSetting<ColorGrading>().hueShift.value = 0f;
        GameManager.Instance.InSpiritWorld = false;
    }

    private IEnumerator ColorTransition(float originalValue, float targetValue, float time)
    {
        float originalTime = time;

        while(time > 0.0f)
        {
            time -= Time.deltaTime;

            postProcess.GetSetting<ColorGrading>().hueShift.value = Mathf.Lerp(originalValue, targetValue, time);
            yield return null;
        }
    }

    private IEnumerator SkyboxTransition(float speed)
    {
        if (GameManager.Instance.InSpiritWorld)
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
