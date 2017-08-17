using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkLiftInput : MonoBehaviour
{

    public float forkVerticalInput;
    public float forkHorizontalInput;

	// Use this for initialization
	void Start ()
	{
	    string[] joy = Input.GetJoystickNames();
	    for (int i = 0; i < joy.Length; i++)
	    {
	        Debug.Log(joy[i]);
	    }

	}
	
	// Update is called once per frame
	void Update ()
	{
	    forkVerticalInput = Input.GetAxisRaw("Vertical");
        forkHorizontalInput = Input.GetAxisRaw("Horizontal");
         
	}
}
