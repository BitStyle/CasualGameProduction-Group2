using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] float scoreValue = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Ring");
            //Do score stuff here
            //Do particle FX here
            //Debug.Log("Get Ring");
           
            Destroy(this.gameObject);
        }
    }
}
