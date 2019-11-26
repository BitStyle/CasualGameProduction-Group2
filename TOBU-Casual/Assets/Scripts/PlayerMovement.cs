using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Config Params
    [SerializeField] float moveSpeedX = 8.0f;
    [SerializeField] float moveSpeedY = 8.0f;
    [SerializeField] float moveSpeedZ = 2.0f;
    [SerializeField] float rotationSpeedZ = 15.0f;
    [SerializeField] float clampBufferX = 1.0f;
    [SerializeField] float clampBufferY = 1.0f;
    [SerializeField] float rotationResetSpeed = 3f;
    [SerializeField] Boolean usingGyroControls = true;
    //Gyroscope Sensitivity
    [SerializeField] bool invertYAxis = false;
    [SerializeField] float gyroSensitivity = 0.0f;
    [SerializeField] float gyroSpeedX = 15;
    [SerializeField] float gyroSpeedZ = 20;
    float gyroOriginX = 0.0f;
    float gyroOriginZ = 0.0f;
    Vector3 baseAcceleration;
    Vector3 relativeAcceleration;

    //InvertGyroButtonTextChange
    [SerializeField] TextMeshProUGUI inverButtonText;

    float minPosX;
    float maxPosX;
    float minPosY;
    float maxPosY;
    float rotationProgress = -1f;

    //Cached Component References
    Rigidbody myRigidbody;
    CapsuleCollider myBodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myBodyCollider = GetComponent<CapsuleCollider>();
        SetMoveBounds();
        //gyroOriginX = PlayerPrefs.GetFloat("GyroOriginX");
        //gyroOriginZ = PlayerPrefs.GetFloat("GyroOriginZ");
        Input.gyro.enabled = false;
        Input.gyro.enabled = true;
        baseAcceleration = Input.acceleration;

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
        ResetRotation();
        ClampRotation();
    }

    private void ResetRotation()
    {
        if(Math.Abs(Input.GetAxisRaw("Horizontal")) < 0.2)
        {
            rotationProgress = 0;
            if (rotationProgress < 1 && rotationProgress >= 0)
            {
                rotationProgress += Time.deltaTime * rotationResetSpeed;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationProgress);
            }
        }
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

    private void ClampRotation()
    {
        float clampedRot = transform.eulerAngles.z;
        if(clampedRot < 0f)
        {
            clampedRot = clampedRot + 360;
        }
        if(clampedRot > 180f)
        {
            clampedRot = Mathf.Max(clampedRot, 360 - 55);
        }
        else
        {
            clampedRot = Mathf.Min(clampedRot, 55);
        }
        Vector3 newRotationVector = new Vector3(0f, 0f, clampedRot);
        Quaternion newRotation = Quaternion.Euler(newRotationVector);
        transform.rotation = newRotation;
    }

    private void MoveXY()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * moveSpeedX;
        float deltaY = Input.GetAxisRaw("Vertical") * moveSpeedY;

        if (usingGyroControls)
        {

            //Gyroscopic input
            relativeAcceleration = Input.acceleration - baseAcceleration;
            deltaX += ((relativeAcceleration.x) * (gyroSpeedX + gyroSensitivity));
            if (invertYAxis)
            {
                //The 0.5f applied to the z acceleration allows the player to stay in the center of the screen vertically when the phone is straight
                deltaY += (relativeAcceleration.z * (gyroSpeedZ + gyroSensitivity));
            }
            else
            {
                //The 0.5f applied to the z acceleration allows the player to stay in the center of the screen vertically when the phone is straight
                deltaY -= (relativeAcceleration.z * (gyroSpeedZ + gyroSensitivity));
            }
        }

        Vector3 velocity = new Vector3(deltaX, deltaY, myRigidbody.velocity.z);
        myRigidbody.velocity = velocity;
        transform.Rotate(0.0f, 0.0f, -(deltaX * Time.deltaTime * rotationSpeedZ));
    }

    private void MoveZ()
    {
        Vector3 velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y, moveSpeedZ);
        myRigidbody.velocity = velocity;
    }

    public void UpdateGyroOrigin()
    {
        gyroOriginX = PlayerPrefs.GetFloat("GyroOriginX");
        gyroOriginZ = PlayerPrefs.GetFloat("GyroOriginZ");
    }

    public void ChangeYAxis()
    {
        if (invertYAxis)
        {
            invertYAxis = false;
            inverButtonText.text = "";
        }
        else if (!invertYAxis)
        {
            invertYAxis = true;
            inverButtonText.text = "X";
        }
    }
}
