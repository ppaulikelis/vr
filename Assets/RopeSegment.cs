using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    [SerializeField] private RopeCut _root;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            _root.Cut();
        }
    }
}
