using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject ringForEffect;
    private IEnumerator coroutine;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        //Not sure where the best place to put this is (initiating background music)
        FindObjectOfType<AudioManager>().Play("BGMusic1");

        //Stop game from sleeping, because gyroscope is not a registered input.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        originalScale = ringForEffect.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsDead)
        {
            Debug.Log("You Died");
            PlayerPrefs.SetFloat("currentScore", GameManager.Instance.Score);
            //SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown("p"))
        {
            PauseGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            GameManager.Instance.IsDead = true;
        }
        else if (collision.collider.tag == "Ring")
        {
            GameManager.Instance.RingCounter++;
            Debug.Log("Ring Count: " + GameManager.Instance.RingCounter);
            RingJuice();
        }
    }

    private void RingJuice()
    {
        ringForEffect.SetActive(true);
        coroutine = LerpScaleDown(0.5f);
        StartCoroutine(coroutine);
    }

    private IEnumerator LerpScaleDown(float time)
    {
        Vector3 targetScale = originalScale * 0.01f;
        float originalTime = time;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;

            ringForEffect.transform.localScale = Vector3.Lerp(targetScale, originalScale, time / originalTime);
            yield return null;
        }
    }

    void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }
}
