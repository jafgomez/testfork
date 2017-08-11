using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoTinesForkController : MonoBehaviour
{
    public TineController.TOUCH_POINTS NumberOfPointToCheck;

    private Rigidbody myRigidbody;

    private bool leftTineCompleted;
    private bool rightTineCompleted;

    private List<GameObject> controlledPallets;

    private PalletController currentPalletController;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        controlledPallets = new List<GameObject>();

    }

    private void OnDestroy()
    {
        controlledPallets.Clear();
        controlledPallets = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        PalletController pc = other.GetComponentInParent<PalletController>();
        if (pc != null)
        {
            if (!controlledPallets.Contains(pc.gameObject))
            {
                controlledPallets.Add(pc.gameObject);
                pc.OnCarryMe += HandleOnCarryMe;
                pc.OnDropIt += HandleOnDropIt;
            }
            pc.SetTouchZone(other.GetComponent<TouchZoneDescriptor>().TouchZone, true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        PalletController pc = other.GetComponentInParent<PalletController>();
        if (pc != null)
        {
            if (controlledPallets.Contains(pc.gameObject))
            {
                controlledPallets.Remove(pc.gameObject);
                pc.OnCarryMe -= HandleOnCarryMe;
                pc.OnDropIt -= HandleOnDropIt;
            }
            pc.SetTouchZone(other.GetComponent<TouchZoneDescriptor>().TouchZone, false);
        }

        //HandleTriggerAction(other.GetComponent<PalletController>(), false);
    }

    private void HandleOnDropIt(PalletController pc)
    {
        //HandleTriggerAction(pc, false);
    }

    private void HandleOnCarryMe(PalletController pc)
    {
        HandleTriggerAction(pc, true);
    }


    private void HandleTriggerAction(PalletController pc, bool isEnter)
    {
        if (pc != null)
        {
            HandleOnTineTouchesCompleted(pc);
        }
    }

    private void HandleOnTineTouchesCompleted(PalletController pc)
    {
        if (pc.AllTouched)
        {
            if (pc.IsOnGround)
            {
                currentPalletController = pc;
                currentPalletController.SetForkRigidbody(myRigidbody);
            }
        }
        else
        {
            currentPalletController.SetForkRigidbody(null);
            currentPalletController = null;
        }
    }
}

