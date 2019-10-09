using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsDead)
        {
            Debug.Log("You Died");
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
}
