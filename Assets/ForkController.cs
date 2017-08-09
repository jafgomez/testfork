using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkController : MonoBehaviour
{

    public Rigidbody fork;

    public float MaxForkLocalHeight = 5f;

    [Range(-40, 40)]
    public float enginePower;

    [Range(-15f, 15f)]
    public float ForkRotation;

    [Range(-0.2f, 0.2f)]
    public float ForkDisplacement;

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

	    Vector3 l = fork.transform.localPosition;
	    l.x = Mathf.Lerp(fork.transform.localPosition.x, ForkDisplacement, 10*Time.deltaTime);
	    fork.transform.localPosition = l;
	    

	}
}
