using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Vector3 offset = new Vector3(0f, 2.46f, -7.65f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 newPos = new Vector3(offset.x, offset.y, targetPos.position.z + offset.z);

        transform.position = newPos;
    }
}
