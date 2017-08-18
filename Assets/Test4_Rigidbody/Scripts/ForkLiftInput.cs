using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkLiftInput : MonoBehaviour
{

    public ForkController ForkController;

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
	    ForkController.ForkYAxisInput = Input.GetAxisRaw("Vertical");
        ForkController.ForkXAxisInput = Input.GetAxisRaw("Horizontal");
        ForkController.ForkZAxisInput = Input.GetAxisRaw("Frontal");

    }
}
