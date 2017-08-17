using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkController : MonoBehaviour
{
    public float maxForce = 20f;
    public ForkLiftInput LiftInput;
    public Transform TopBlocker;
    public Transform BottomBlocker;
    public Transform CenterOfMass;

    [SerializeField]
    private float _upForce;
    private Vector3 _topBlockerOffset;
    private Vector3 _bottomBlockerOffset;
    private Rigidbody _myRigidBody;
    [SerializeField]
    private float _rawVerticalInput;


    private void Awake()
    {
        _topBlockerOffset = transform.position - TopBlocker.position;
        _bottomBlockerOffset = transform.position - BottomBlocker.position;
        _myRigidBody = GetComponent<Rigidbody>();
        _myRigidBody.centerOfMass = CenterOfMass.localPosition;
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	    _rawVerticalInput = LiftInput.forkVerticalInput * .25f;
	    float rawHorizontalInput = LiftInput.forkHorizontalInput;

	    if (_rawVerticalInput != 0.0f)
	    {
            if (_rawVerticalInput < 0f)
            {
                _upForce = -1f;
            }
            else
            {
                if (_upForce <= maxForce)
                    _upForce = maxForce;
            }

            TopBlocker.position = transform.position + _topBlockerOffset;
            BottomBlocker.position = transform.position - _bottomBlockerOffset;


	    }
	    else
	    {
	        _upForce = 0f;
	    }






	}

    private void FixedUpdate()
    {
        if (_upForce != 0f)
            _myRigidBody.AddForce(Vector3.up * _upForce, ForceMode.Force);
    }


}
