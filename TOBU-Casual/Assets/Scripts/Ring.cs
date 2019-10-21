using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] float scoreValue = 0;
    [SerializeField] GameObject particles;

    private void ActivateParticleSystem()
    {
        particles.GetComponent<ParticleSystem>().Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Ring");
            //Do score stuff here
            //Do particle FX here
            this.gameObject.SetActive(false);
            Instantiate(particles, this.transform.position,this.transform.rotation);
            ActivateParticleSystem();
            Debug.Log("Get Ring");
            Destroy(this.gameObject, 2f);
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
        }
    }
}
