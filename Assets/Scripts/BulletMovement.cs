using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speedThreshold = 1f;

    private Rigidbody rb;
    private Transform tr;
    private bool stopped;
    //private Vector3 lastPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        if(rb.velocity.magnitude > speedThreshold)
        tr.LookAt(tr.position + rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //tr.position = lastPos;
        tr.rotation = Quaternion.LookRotation(-collision.contacts[0].normal);
        tr.parent = collision.transform;
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!stopped)
        {
            tr.Translate(collision.contacts[0].normal * 0.05f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        stopped = true;
    }

    private void FixedUpdate()
    {
        //lastPos = tr.position;
    }
}
