using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectDerender : MonoBehaviour
{
    //Camera cam;
    Transform objectHit;

    void Start()
    {
        //cam = GetComponent<Camera>();
    }

    void Update()
    {
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit))
        //{
        //    objectHit = hit.transform;

        //    if (objectHit.tag == "Obstacle")
        //    {
        //        objectHit.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
        //        Color color = objectHit.GetComponent<Renderer>().material.color;
        //        color.a = 0.2f;
        //        objectHit.GetComponent<Renderer>().material.color = color;
        //    }

        //}

        //if (objectHit != null && objectHit.tag == "Obstacle")
        //{
        //    objectHit.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
        //    Color color = objectHit.GetComponent<Renderer>().material.color;
        //    color.a = 1f;
        //    objectHit.GetComponent<Renderer>().material.color = color;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        objectHit = collision.transform;
        if (objectHit.tag == "Obstacle")
        {
            objectHit.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
            Color color = objectHit.GetComponent<Renderer>().material.color;
            color.a = 0.2f;
            objectHit.GetComponent<Renderer>().material.color = color;
        }
    }
}
