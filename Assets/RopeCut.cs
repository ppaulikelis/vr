using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RopeCut : MonoBehaviour
{
    [SerializeField] private Rigidbody _firstSegment;
    [SerializeField] private InteractionLayerMask _layerMaskAfterCut;

    private XRGrabInteractable[] _interactables;
    private Collider[] _colliders;
    private bool _isCut;

    private void Start()
    {
        _interactables = GetComponentsInChildren<XRGrabInteractable>();
        _colliders = GetComponentsInChildren<Collider>();
    }

    public void Cut()
    {
        if (!_isCut)
        {
            _isCut = true;
            StartCoroutine(DisableColliders());
            _firstSegment.isKinematic = false;
            foreach (var interactable in _interactables)
            {
                interactable.interactionLayers = _layerMaskAfterCut;
            }
        }
    }

    private IEnumerator DisableColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
        yield return new WaitForSeconds(0.25f);
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
    } 
}
