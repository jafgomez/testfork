using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkController : MonoBehaviour
{

    public Rigidbody fork;

    public float MaxForkLocalHeight = 5f;

    [Range(0, 40)]
    public float enginePower;

    [Range(-15, 15)]
    public int ForkRotation;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	    if (fork.transform.localPosition.y >= MaxForkLocalHeight)
	        enginePower = 0f;

        fork.AddForce(fork.transform.up * enginePower, ForceMode.Force);

	    fork.transform.localRotation = Quaternion.Slerp(fork.transform.localRotation,
	        Quaternion.AngleAxis(ForkRotation, Vector3.right), 10*Time.deltaTime);

	}
}
