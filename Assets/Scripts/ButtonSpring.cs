using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ButtonSpring : MonoBehaviour
{
    [SerializeField] private Axis allowedAxis;
    [SerializeField] private float movementLimit;
    [SerializeField] private bool invert;
    [SerializeField] private float returnForce;
    [SerializeField] private float pressedOffset = 0f;

    public bool IsPressed;
    private bool allowNewPress = false;

    private Transform trans;
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 maxOffset;

    int invertInt = 1;

    private void Start()
    {
        trans = transform;
        rb = GetComponent<Rigidbody>();
        startPos = trans.position;
        if (invert)
        {
            invertInt = -1;
        }

        switch (allowedAxis)
        {
            case Axis.X:
                maxOffset = startPos + movementLimit * invertInt * new Vector3(1, 0, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                break;
            case Axis.Y:
                maxOffset = startPos + movementLimit * invertInt * new Vector3(0, 1, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                break;
            case Axis.Z:
                maxOffset = startPos + movementLimit * invertInt * new Vector3(0, 0, 1);
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                break;
        }
    }

    private void Pressed()
    {
        if (!IsPressed && allowNewPress)
        {
            StartCoroutine(StartIdleTimer(1f));
        }
    }

    private IEnumerator StartIdleTimer(float seconds)
    {
        allowNewPress = false;
        IsPressed = true;
        yield return new WaitForEndOfFrame();
        IsPressed = false;
        yield return new WaitForSeconds(seconds);
        allowNewPress = true;
    }

    private void FixedUpdate()
    {
        if (allowedAxis == Axis.X)
        {
            if (trans.position.x * invertInt > maxOffset.x * invertInt)
            {
                trans.position = maxOffset;
            }
            if (trans.position.x * invertInt > (maxOffset.x + (-pressedOffset * invertInt)) * invertInt)
            {
                Pressed();
            }
            if (trans.position.x * invertInt < startPos.x * invertInt)
            {
                trans.position = startPos;
                rb.velocity = Vector3.zero;
                allowNewPress= true;
            }
        }
        else if (allowedAxis == Axis.Y)
        {
            if (trans.position.y * invertInt > maxOffset.y * invertInt)
            {
                trans.position = maxOffset;
            }
            if (trans.position.y * invertInt > (maxOffset.y + (-pressedOffset * invertInt)) * invertInt)
            {
                Pressed();
            }
            if (trans.position.y * invertInt < startPos.y * invertInt)
            {
                trans.position = startPos;
                rb.velocity = Vector3.zero;
                allowNewPress = true;
            }
        }
        else
        {
            if (trans.position.z * invertInt > maxOffset.z * invertInt)
            {
                trans.position = maxOffset;
            }
            if(trans.position.z * invertInt > (maxOffset.z + (-pressedOffset * invertInt)) * invertInt)
            {
                Pressed();
            }
            if (trans.position.z * invertInt < startPos.z * invertInt)
            {
                trans.position = startPos;
                rb.velocity = Vector3.zero;
                allowNewPress = true;
            }
        }

        rb.AddForce((startPos - trans.position) * returnForce, ForceMode.Acceleration);
    }
}


enum Axis
{
    X,
    Y,
    Z
};

