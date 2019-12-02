using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScreenTextPulse : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadText;
    private bool faded = false;
    [SerializeField] float duration;
    [SerializeField] float waitTime;
    AsyncOperation asyncLoad;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(FadeInAndOutRepeat(duration,waitTime));
    }

    private void Start()
    {
        StartCoroutine(LoadGameScene());
    }
    // Update is called once per frame
    void Update()
    {
      
    }

    //Pulse alpha forever
    IEnumerator FadeInAndOutRepeat(float duration, float waitTime)
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (true)
        {
            
            //Fade out
            yield return fadeInAndOut(false, duration);

            //Wait
            yield return waitForXSec;

            //Fade-in 
            yield return fadeInAndOut(true, duration);
            asyncLoad.allowSceneActivation = true;
        }
    }

    IEnumerator fadeInAndOut(bool fadeIn, float duration)
    {
        float minAlpha = 0; // min 
        float maxAlpha = 1; // max 

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minAlpha;
            b = maxAlpha;
        }
        else
        {
            a = maxAlpha;
            b = minAlpha;
        }

        float currentAlpha = loadText.color.a;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            Color c = loadText.color;
            c.a = Mathf.Lerp(a, b, counter / duration);
            loadText.color = c;

            yield return null;
        }
    }

    IEnumerator LoadGameScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync("MainGame", LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        asyncLoad.priority = 9;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
