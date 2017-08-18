using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkController : MonoBehaviour
{
    public ForkDescriptor ForkDescriptor;
    public Transform LogicFork;
    public float SpeedTranslateFactor; //Platform travel speed
    public Vector3 MaxTranslation; //The maximum translation limit of the platform
    public Vector3 MinTranslation; //The minimum translation limit of the platform

    public float ForkYAxisInput;
    public float ForkXAxisInput;
    public float ForkZAxisInput;
    public float MastInput;
    
    private Rigidbody _logicForkRigidbody;
    private float _initialForkHeight;
    private float _initialForkFront;


    private void Awake()
    {
        _logicForkRigidbody = LogicFork.GetComponent<Rigidbody>();

        _initialForkHeight = ForkDescriptor.Fork.localPosition.y;
        _initialForkFront = ForkDescriptor.Fork.localPosition.z;
        
    }
    

    private void FixedUpdate()
    {

        float rawYInput = ForkYAxisInput;
        float rawXInput = ForkXAxisInput;
        float rawZInput = ForkZAxisInput;
        

        
        float factorY = SpeedTranslateFactor*rawYInput*Time.deltaTime;
        float factorX = SpeedTranslateFactor*rawXInput*Time.deltaTime;
        float factorZ = SpeedTranslateFactor*rawZInput*Time.deltaTime;

        if (ForkDescriptor.Fork.localPosition.y + factorY > MaxTranslation.y || ForkDescriptor.Fork.localPosition.y + factorY <= MinTranslation.y + _initialForkHeight)
            factorY = 0f;

        if (ForkDescriptor.Fork.localPosition.x + factorX > MaxTranslation.x || ForkDescriptor.Fork.localPosition.x + factorX <= MinTranslation.x)
            factorX = 0f;

        if (ForkDescriptor.Fork.localPosition.z + factorZ > MaxTranslation.z + _initialForkFront || ForkDescriptor.Fork.localPosition.z + factorZ <= MinTranslation.z + _initialForkFront)
            factorZ = 0f;



        Vector3 translation = new Vector3(factorX, factorY, factorZ);
        Vector3 logicPosition = translation + _logicForkRigidbody.position;

        _logicForkRigidbody.MovePosition(logicPosition);
        ForkDescriptor.Fork.Translate(translation);


        //if (mastMoveTrue)
        //{
        //    mast.Translate(Vector3.up * speedTranslate * Time.deltaTime);
        //}



    }


}
