using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Config Params
    [SerializeField] float moveSpeedX = 8.0f;
    [SerializeField] float moveSpeedY = 8.0f;
    [SerializeField] float moveSpeedZ = 2.0f;
    [SerializeField] float clampBufferX = 1.0f;
    [SerializeField] float clampBufferY = 1.0f;
    //Gyroscope Sensitivity
    [SerializeField] float gyroSensitivity = 0.0f;
    [SerializeField] float gyroSpeedX = 15;
    [SerializeField] float gyroSpeedZ = 20;


    float minPosX;
    float maxPosX;
    float minPosY;
    float maxPosY;

    //Cached Component References
    Rigidbody myRigidbody;
    CapsuleCollider myBodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myBodyCollider = GetComponent<CapsuleCollider>();
        SetMoveBounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveXY();
        MoveZ();
    }

    private void LateUpdate()
    {
        ClampMovement();
    }

    private void SetMoveBounds()
    {
        Camera mainCamera = Camera.main;
        float cameraDist = transform.position.z - mainCamera.transform.position.z;

        minPosX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, cameraDist)).x + clampBufferX;
        maxPosX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, cameraDist)).x - clampBufferX;
        minPosY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, cameraDist)).y + clampBufferY;
        maxPosY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, cameraDist)).y - clampBufferY;
    }

    private void ClampMovement()
    {
        Vector3 clampedPos = transform.position;

        clampedPos.x = Mathf.Clamp(clampedPos.x, minPosX, maxPosX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minPosY, maxPosY);
        transform.position = clampedPos;
    }

    private void MoveXY()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * moveSpeedX;
        float deltaY = Input.GetAxisRaw("Vertical") * moveSpeedY;
        //Gyroscopid input
        deltaX += (Input.acceleration.x * (gyroSpeedX + gyroSensitivity));
        //The 0.5f applied to the z acceleration allows the player to stay in the center of the screen vertically when the phone is straight
        deltaY += ((Input.acceleration.z + 0.5f) * (gyroSpeedZ + gyroSensitivity));

        Vector3 velocity = new Vector3(deltaX, deltaY, myRigidbody.velocity.z);
        myRigidbody.velocity = velocity;
    }

    private void MoveZ()
    {
        Vector3 velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y, moveSpeedZ);
        myRigidbody.velocity = velocity;
    }  
}
