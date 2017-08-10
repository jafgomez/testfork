using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicBoxController : MonoBehaviour
{

    private Rigidbody ownRigidbody;
    private Rigidbody collisionRigidbody;

    private void Awake()
    {
        ownRigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //ownRigidbody.isKinematic = true;
        collisionRigidbody = collision.rigidbody;
    }

    private void OnCollisionExit(Collision collision)
    {
        //ownRigidbody.isKinematic = false;
        collisionRigidbody = null;
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void FixedUpdate()
    {
        if (collisionRigidbody == null) return;

        Vector3 offset = new Vector3(0f, (collisionRigidbody.centerOfMass.y/2) + ownRigidbody.centerOfMass.y/2, 0f);

        ownRigidbody.MovePosition(collisionRigidbody.position + offset * Time.deltaTime);
    }


}
