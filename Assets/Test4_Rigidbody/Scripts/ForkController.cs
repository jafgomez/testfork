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
    [SerializeField]
    private float _initialForkHeight;
    [SerializeField]
    private float _deltaForkHeight;

    private void Awake()
    {
        _logicForkRigidbody = LogicFork.GetComponent<Rigidbody>();

        _initialForkHeight = ForkDescriptor.Fork.localPosition.y;
        
    }
    

    private void FixedUpdate()
    {

        float rawVerticalInput = LiftInput.forkVerticalInput;
        float rawHorizontalInput = LiftInput.forkHorizontalInput;

        
        float factorV = SpeedTranslateFactor*rawVerticalInput*Time.deltaTime;
        float factorH = SpeedTranslateFactor*rawHorizontalInput*Time.deltaTime;

        if (ForkDescriptor.Fork.localPosition.y + factorV > MaxTranslation.y || ForkDescriptor.Fork.localPosition.y + factorV <= MinTranslation.y + _initialForkHeight)
            factorV = 0f;

        if (ForkDescriptor.Fork.localPosition.x + factorH > MaxTranslation.x || ForkDescriptor.Fork.localPosition.x + factorH <= MinTranslation.x)
            factorH = 0f;



        Vector3 translation = new Vector3(factorH, factorV, 0f);
        Vector3 logicPosition = translation + _logicForkRigidbody.position;

        _logicForkRigidbody.MovePosition(logicPosition);
        ForkDescriptor.Fork.Translate(translation);


        //if (mastMoveTrue)
        //{
        //    mast.Translate(Vector3.up * speedTranslate * Time.deltaTime);
        //}



    }


}
