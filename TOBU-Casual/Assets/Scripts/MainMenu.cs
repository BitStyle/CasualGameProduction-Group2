using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //PlayerPrefs.SetFloat("GyroOriginX", Input.acceleration.x);
        //PlayerPrefs.SetFloat("GyroOriginZ", Input.acceleration.z);
        SceneManager.LoadScene("LoadingScreen");
    }

    public void ReturntoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        //Application.Quit();
    }
}
