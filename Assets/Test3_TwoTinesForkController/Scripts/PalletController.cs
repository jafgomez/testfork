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
    public event Action<PalletController> OnCarryMe;
    public event Action<PalletController> OnDropIt;

    public bool IsOnGround;
    [NonSerialized]
    public Rigidbody ForkRigidbody;

    private Rigidbody myRigidbody;
    private bool[] touches_zones = new bool[4];
    private Vector3 palletOffset;
    private bool eventLaunched;

    private TouchZoneDescriptor[] touchZones;

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

    public void SetTouchZone(TOUCHES_ZONES zone, bool isEnter)
    {
        touches_zones[(int) zone] = isEnter;
        if (AllTouched)
        {
            if (OnCarryMe != null) OnCarryMe(this);
            eventLaunched = false;
            EnableTouchZone(false);
            SetRigidbodyState();
        }
        else
        {
            if (!eventLaunched && !isEnter)
            {
                eventLaunched = true;
                if (OnDropIt != null) OnDropIt(this);
                SetRigidbodyState();
            }
        }
    }



    public void SetForkRigidbody(Rigidbody forkRigidbody)
    {
        palletOffset = transform.position - forkRigidbody.transform.position;
        ForkRigidbody = forkRigidbody;
    }

    private void SetRigidbodyState()
    {
        if (AllTouched)
        {
            if (IsOnGround)
            {
                myRigidbody.isKinematic = true;
            }
        }
        else
        {
            myRigidbody.isKinematic = false;
        }
    }

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        TouchZoneDescriptor[] tzs = GetComponentsInChildren<TouchZoneDescriptor>();

        touchZones = new TouchZoneDescriptor[tzs.Length];

        for (int i = 0 ; i < tzs.Length ; i++)
        {
            touchZones[i] = tzs[i];
        }

    }



    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            myRigidbody.isKinematic = false;
            EnableTouchZone(true);
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

    private void FixedUpdate()
    {
        if (ForkRigidbody != null)
        {
            Vector3 localOffset = ForkRigidbody.transform.TransformPoint(palletOffset); 
            myRigidbody.MovePosition(localOffset);
            myRigidbody.MoveRotation(ForkRigidbody.rotation);

        }
    }

    private void EnableTouchZone(bool enable)
    {
        for (int i = 0; i < touchZones.Length; i++)
        {
            touchZones[i].gameObject.SetActive(enable);
        }
    }

}
