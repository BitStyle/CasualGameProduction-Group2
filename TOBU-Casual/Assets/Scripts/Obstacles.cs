using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            
        }
    }
}
