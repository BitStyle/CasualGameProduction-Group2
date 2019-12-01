using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingDestroyer : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Vector3 offset = new Vector3(0f, 0f, -5f);

    void LateUpdate()
    {
        FollowTarget();     //I call this one... a pro gamer move
    }

    private void FollowTarget()
    {
        Vector3 newPos = new Vector3(offset.x, offset.y, targetPos.position.z + offset.z);

        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.RingCounter = 0;
            GameManager.Instance.InSpiritWorld = false;
        }
    }
}
