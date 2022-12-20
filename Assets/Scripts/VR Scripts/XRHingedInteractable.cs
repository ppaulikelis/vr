using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class XRHingedInteractable : XRGrabInteractable
{
    [SerializeField] private float distance = 0.1f;
    [SerializeField] private XRBaseInteractor leftInteractor;
    [SerializeField] private XRBaseInteractor rightInteractor;

    //private Vector3 attachPosition;

    private void Start()
    {
        if (leftInteractor == null)
            Debug.LogWarning($"'leftInteractor' null in {this.name}");
        if (rightInteractor == null)
            Debug.LogWarning($"'rightIneractor' null in {this.name}");
    }

    //private void Update()
    //{
    //    if (InteractorTooFarFromInteractable(leftInteractor))
    //        StartCoroutine(DropInteractable(leftInteractor));

    //    if (InteractorTooFarFromInteractable(rightInteractor))
    //        StartCoroutine(DropInteractable(rightInteractor));
    //}

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        StartCoroutine(DropIfTooFar(interactor));
    }

    private IEnumerator DropIfTooFar(XRBaseInteractor interactor)
    {
        Vector3 lastPosition = interactor.transform.position;
        yield return new WaitForEndOfFrame();
        while (interactor.firstInteractableSelected != null && Vector3.Distance(interactor.transform.position, lastPosition) < distance)
        {
            lastPosition = interactor.transform.position;
            yield return new WaitForSeconds(0.1f);
        }
        interactor.allowSelect = false;
        yield return new WaitForSeconds(0.1f);
        interactor.allowSelect = true;
    }

    //private IEnumerator DropInteractable(XRBaseInteractor interactor)
    //{
    //    dropRunning = true;

    //    interactor.allowSelect = false;
    //    yield return new WaitForSeconds(0.1f);
    //    interactor.allowSelect = true;

    //    dropRunning = false;
    //}

    //private bool InteractorTooFarFromInteractable(XRBaseInteractor interactor)
    //{
    //    return (interactor.firstInteractableSelected != null && Vector3.Distance(interactor.attachTransform.position, attachPosition) >= distance);
    //}
    
}
