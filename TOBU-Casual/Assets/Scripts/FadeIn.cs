using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        Color color = rend.material.color;
        color.a = 0f;
        rend.material.color = color;
        IEnumerator coroutine = FadeInCoroutine();
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeInCoroutine()
    {
        for(float time = 0.05f; time <= 1; time += 0.05f)
        {
            Color color = rend.material.color;
            color.a = time;
            rend.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
