using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunSlide : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private Transform slideCenter;
    [SerializeField] private float movementLimit;
    [SerializeField] private float slideDetectionRadius = 0.1f;
    [SerializeField] private FireBulletOnActivate fireScript;

    [SerializeField] InputActionProperty leftHandSelectAction;
    [SerializeField] InputActionProperty rightHandSelectAction;
    [SerializeField] XRDirectInteractor leftInteractor;
    [SerializeField] XRDirectInteractor rightInteractor;
    [SerializeField] XrGrabInteractableToAttach attach;

    [SerializeField] XRRayInteractor leftTeleportRay;
    [SerializeField] XRRayInteractor rightTeleportRay;

    private Transform tr;
    private Vector3 startPos;
    private Vector3 maxOffset;

    private float forwardOffset;
    private bool coroutineRunning = false;

    private void Start()
    {
        tr = transform;
    }

    private void Update()
    {
        if(attach.interactorsSelecting.Count != 0)
        {
            if (!attach.interactorsSelecting[0].transform.tag.Equals("Left Hand") && Vector3.Distance(leftInteractor.transform.position, slideCenter.position) < slideDetectionRadius)
            {
                var leftHandValue = leftHandSelectAction.action?.ReadValue<float>() ?? 0.0f;
                if (!coroutineRunning && leftHandValue != 0.0f)
                {
                    StartCoroutine(GrabCoroutine(leftHandSelectAction, leftInteractor.transform));
                }
            }
            if (!attach.interactorsSelecting[0].transform.tag.Equals("Right Hand") && Vector3.Distance(rightInteractor.transform.position, slideCenter.position) < slideDetectionRadius)
            {
                var rightHandValue = rightHandSelectAction.action?.ReadValue<float>() ?? 0.0f;
                if (!coroutineRunning && rightHandValue != 0.0f)
                {
                    StartCoroutine(GrabCoroutine(rightHandSelectAction, rightInteractor.transform));
                }
            }
        }
    }

    private IEnumerator GrabCoroutine(InputActionProperty inputAction, Transform handTransform)
    {
        leftTeleportRay.enabled = false;
        rightTeleportRay.enabled = false;
        yield return new WaitForEndOfFrame();
        coroutineRunning = true;
        var inputValue = inputAction.action?.ReadValue<float>() ?? 0.0f;
        Vector3 handPos = handTransform.position;
        while (inputValue > 0)
        {
            yield return new WaitForFixedUpdate();
            inputValue = inputAction.action?.ReadValue<float>() ?? 0.0f;

            forwardOffset = Vector3.Distance(handPos,handTransform.position) * Vector3.Dot(tr.forward, handPos - handTransform.position);
        }

        forwardOffset = 0;
        leftTeleportRay.enabled = true;
        rightTeleportRay.enabled = true;
        coroutineRunning = false;
    }

    private void FixedUpdate()
    {
        tr.rotation = gun.rotation;
        tr.localPosition = tr.localPosition + new Vector3(0, 0, 1) * -forwardOffset;

        if (tr.localPosition.z < movementLimit)
        {
            tr.localPosition = movementLimit * new Vector3(0, 0, 1);
            fireScript.isReloaded = true;
        }
        if (tr.localPosition.z >= 0)
        {
            tr.localPosition = Vector3.zero;
            if(fireScript.isReloaded)
                fireScript.isReset = true;
        }
    }
}
