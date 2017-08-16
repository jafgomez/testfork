using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float Force;

    public Transform CenterOfMass;
    private Rigidbody myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.centerOfMass = CenterOfMass.position;
    }

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(Push());
	}

    IEnumerator Push()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            
        }
    }

	// Update is called once per frame
    void Update ()
    {

        if (Input.GetKey(KeyCode.UpArrow))
            Force += 0.1f;

        if (Input.GetKey(KeyCode.DownArrow))
            Force -= 0.1f;

        myRigidbody.AddForce(transform.up * Force, ForceMode.Force);
    }
}
