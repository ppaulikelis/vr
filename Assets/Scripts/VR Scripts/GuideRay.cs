using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuideRay : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _length;

    private Transform _transform;

    private void Start()
    {
        _transform = this.transform;
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, _transform.position);
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, _transform.forward, out hit, _length)) 
        {

            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, _transform.position + _transform.forward * _length);
        }
    }


}
