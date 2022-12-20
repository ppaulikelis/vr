using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkWater : MonoBehaviour
{
    [SerializeField] private float _rotateToAngle = 15;
    [SerializeField] private ParticleSystem _water;

    private Vector3 _startingForward;

    private void Start()
    {
        _startingForward = this.transform.forward;
    }

    private void Update()
    {
        if (Vector3.Angle(_startingForward, transform.forward) >= _rotateToAngle) 
        {
            if (!_water.isPlaying)
            {
                _water.Play();
            }
        }
        else
        {
            _water.Stop();
        }
   
    }
}
