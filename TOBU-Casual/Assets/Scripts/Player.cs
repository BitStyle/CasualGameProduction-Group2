﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Not sure where the best place to put this is (initiating background music)
        FindObjectOfType<AudioManager>().Play("BGMusic1");

        //Stop game from sleeping, because gyroscope is not a registered input.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsDead)
        {
            Debug.Log("You Died");
            SceneManager.LoadScene(2);
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
        }
    }

    void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }
}