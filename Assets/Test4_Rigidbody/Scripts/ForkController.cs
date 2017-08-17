using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkController : MonoBehaviour
{
    public ForkLiftInput LiftInput;
    public ForkDescriptor ForkDescriptor;
    public Transform LogicFork;
    public float SpeedTranslateFactor; //Platform travel speed
    public Vector3 MaxTranslation; //The maximum translation limit of the platform
    public Vector3 MinTranslation; //The minimum translation limit of the platform
    public float MaxYmast; //The maximum height of the mast
    public float MinYmast; //The minimum height of the mast
    private Rigidbody _logicForkRigidbody;
    
    private void Awake()
    {
        _logicForkRigidbody = LogicFork.GetComponent<Rigidbody>();
    }
    

    private void FixedUpdate()
    {

        float rawVerticalInput = LiftInput.forkVerticalInput;
        float rawHorizontalInput = LiftInput.forkHorizontalInput;

        
        float factorV = SpeedTranslateFactor*rawVerticalInput*Time.deltaTime;
        float factorH = SpeedTranslateFactor*rawHorizontalInput*Time.deltaTime;

        Vector3 translation = new Vector3(0f, factorV, factorH);
        
        Vector3 logicPosition = translation + _logicForkRigidbody.position;

        _logicForkRigidbody.MovePosition(logicPosition);
        ForkDescriptor.Fork.Translate(translation);
        
        //if (mastMoveTrue)
        //{
        //    mast.Translate(Vector3.up * speedTranslate * Time.deltaTime);
        //}



    }


}
