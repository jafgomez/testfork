using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoTinesForkController : MonoBehaviour
{

    public TineController LeftTine;
    public TineController RightTine;

    public TineController.TOUCH_POINTS NumberOfPointToCheck;
    
    private bool leftTineCompleted;
    private bool rightTineCompleted;

    private void Awake()
    {

        LeftTine.NumberOfPointToCheck = NumberOfPointToCheck;
        RightTine.NumberOfPointToCheck = NumberOfPointToCheck;

        LeftTine.OnTineTouchesCompleted += HandleOnTineTouchesCompleted;
        LeftTine.OnTineTouchesIncompleted += HandleOnTineTouchesIncompleted;

        RightTine.OnTineTouchesCompleted += HandleOnTineTouchesCompleted;
        RightTine.OnTineTouchesIncompleted += HandleOnTineTouchesIncompleted;
    }

    private void OnDestroy()
    {
        LeftTine.OnTineTouchesCompleted -= HandleOnTineTouchesCompleted;
        LeftTine.OnTineTouchesIncompleted -= HandleOnTineTouchesIncompleted;

        RightTine.OnTineTouchesCompleted -= HandleOnTineTouchesCompleted;
        RightTine.OnTineTouchesIncompleted -= HandleOnTineTouchesIncompleted;
    }



    private void HandleOnTineTouchesIncompleted(TineController tineController, GameObject collisionGameObject)
    {
        if (tineController == LeftTine)
            leftTineCompleted = false;

        if (tineController == RightTine)
            rightTineCompleted = false;

        if (!leftTineCompleted && !rightTineCompleted)
        {
            Rigidbody r = collisionGameObject.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.isKinematic = false;
                r.useGravity = false;
            }
        }
    }

    private void HandleOnTineTouchesCompleted(TineController tineController, GameObject collisionGameObject)
    {
        if (tineController == LeftTine)
            leftTineCompleted = true;

        if (tineController == RightTine)
            rightTineCompleted = true;

        PalletController pc = collisionGameObject.GetComponent<PalletController>();
        if (pc == null) return;

        pc.OnGround += HandleOnGround;

        if (leftTineCompleted && rightTineCompleted && !pc.IsOnGround)
        {
            Rigidbody r = collisionGameObject.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.isKinematic = true;
                r.useGravity = true;
            }

            
        }
    }

    private void HandleOnGround(PalletController obj)
    {
        Rigidbody r = obj.GetComponent<Rigidbody>();
        if (r != null)
            r.isKinematic = false;
    }
}

