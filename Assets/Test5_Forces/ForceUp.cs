using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUp : MonoBehaviour {
    public float ForkYAxisInput { get; set; }
    public float ForkXAxisInput { get; set; }
    public float ForkZAxisInput { get; set; }
    public float MaxAppliedForce = 100f;

    public float currentForceNM;

    private Rigidbody m_cachedRigidBody;
    private bool m_IWantToStop;
    private Vector3 m_stopPosition;
    private Vector3 m_previousRigidbodyPosition;
    private Action<float> currentLiftAction;

    public Vector3 DebugRigidBodyVelocity;

    void Awake()
    {
        m_cachedRigidBody = GetComponent<Rigidbody>();
    }

    // Use this for initialization

    void Start ()
	{
	    m_previousRigidbodyPosition = m_cachedRigidBody.position;
	}

    // Update is called once per frame

    void FixedUpdate () {

        DebugRigidBodyVelocity = m_cachedRigidBody.velocity;

        float liftInput = ForkYAxisInput;
        currentLiftAction = EvaluateLiftInput(liftInput);

        if (currentLiftAction != null)
            currentLiftAction(liftInput);

    }

    private Action<float> EvaluateLiftInput(float liftInput)
    {
        
        if (liftInput < 0f)
        {
            return LiftDown;
        }
        else if (liftInput > 0f)
        {
            return LiftUp;
        }
        else
        {
            return LiftStay;
            m_IWantToStop = true;
        }
    }

    void LiftUp(float liftInput)
    {
        currentForceNM = Mathf.Clamp(currentForceNM + (liftInput), 0f, MaxAppliedForce);
        m_cachedRigidBody.AddForce(Vector3.up * currentForceNM, ForceMode.VelocityChange);
    }

    void LiftDown(float liftInput)
    {
        currentForceNM = MaxAppliedForce/2f;
        m_cachedRigidBody.AddForce(Vector3.up * currentForceNM, ForceMode.Force);
    }

    void LiftStay(float liftInput)
    {
        
    }
}
