using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor _leftDirectGrab;
    [SerializeField] private XRDirectInteractor _rightDirectGrab;

    [SerializeField] private XRRayInteractor _leftRayGrab;
    [SerializeField] private XRRayInteractor _rightRayGrab;

    [SerializeField] private GameObject _leftGuideRay;
    [SerializeField] private GameObject _rightGuideRay;

    [SerializeField] private InputActionProperty _leftGrabAction;
    [SerializeField] private InputActionProperty _rightGrabAction;

    private void Update()
    {
        if (_leftDirectGrab.interactablesSelected.Count != 0)
        {
            _leftRayGrab.gameObject.SetActive(false);
            _leftGuideRay.SetActive(false);
        }
        else if (_leftRayGrab.interactablesSelected.Count != 0)
        {
            _leftDirectGrab.enabled = false;
            _leftGuideRay.SetActive(false);
        }
        else
        {
            _leftRayGrab.gameObject.SetActive(true);
            _leftDirectGrab.enabled = true;
            _leftGuideRay.SetActive(_leftGrabAction.action.ReadValue<float>() > 0);
        }

        if (_rightDirectGrab.interactablesSelected.Count != 0)
        {
            _rightRayGrab.gameObject.SetActive(false);
            _rightGuideRay.SetActive(false);
        }
        else if (_rightRayGrab.interactablesSelected.Count != 0)
        {
            _rightDirectGrab.enabled = false;
            _rightGuideRay.SetActive(false);
        }
        else
        {
            _rightRayGrab.gameObject.SetActive(true);
            _rightDirectGrab.enabled = true;
            _rightGuideRay.SetActive(_rightGrabAction.action.ReadValue<float>() > 0);
        }
    }
}
