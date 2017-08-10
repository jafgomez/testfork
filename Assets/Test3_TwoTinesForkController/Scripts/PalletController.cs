using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletController : MonoBehaviour
{
    public enum TOUCHES_ZONES
    {
        FRONT_LEFT,
        FRONT_RIGHT,
        BACK_LEFT,
        BACK_RIGHT,
    }

    public event Action<PalletController> OnGround;
    public bool IsOnGround;

    private bool[] touches_zones = new bool[4];

    public bool AllTouched
    {
        get
        {
            for (int i = 0; i < touches_zones.Length; i++)
            {
                if (touches_zones[i] == false)
                    return false;
            }

            return true;
        }
    }

    public void SetTouchZone(TOUCHES_ZONES zone, bool val)
    {
        touches_zones[(int) zone] = val;
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            if (OnGround != null) OnGround(this);
        }
    }

    // OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = false;
        }
    }
    
}
