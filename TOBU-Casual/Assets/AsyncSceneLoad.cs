using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadGameScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator LoadGameScene()
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame", LoadSceneMode.Single);
    //    asyncLoad.priority = 9;

    //    // Wait until the asynchronous scene fully loads
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }
    //}
}
