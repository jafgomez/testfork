using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletCollisionController : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);

            Debug.Log(contact.separation);

        }

    }
}
