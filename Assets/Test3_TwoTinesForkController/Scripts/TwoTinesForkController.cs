using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoTinesForkController : MonoBehaviour
{
    public TineController.TOUCH_POINTS NumberOfPointToCheck;
    
    private bool leftTineCompleted;
    private bool rightTineCompleted;

    private InstanceCounterDictionary<GameObject> touchesByGameObject;

    private void Awake()
    {
        touchesByGameObject = new InstanceCounterDictionary<GameObject>();
    }

    private void OnDestroy()
    {
        touchesByGameObject.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject pallet = other.transform.parent.gameObject;

        if (pallet.name.Contains("Pallet"))
        {
            HandleOnTineTouchesCompleted(pallet);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    GameObject pallet = other.transform.parent.gameObject;

    //    if (pallet.name.Contains("Pallet"))
    //    {
    //        HandleOnTineTouchesIncompleted(pallet);
    //    }
    //}

    private void HandleOnTineTouchesCompleted(GameObject collisionGameObject)
    {

        PalletController pc = collisionGameObject.GetComponent<PalletController>();
        if (pc == null) return;

        int touches = touchesByGameObject.Add(collisionGameObject);
        
        if (touches == (int)NumberOfPointToCheck)
        {
            Rigidbody r = collisionGameObject.GetComponent<Rigidbody>();
            if (r != null && pc.IsOnGround)
            {
                r.isKinematic = true;
            }
            
            pc.OnGround += HandleOnGround;
        }
    }

    private void HandleOnTineTouchesIncompleted(GameObject collisionGameObject)
    {

        PalletController pc = collisionGameObject.GetComponent<PalletController>();
        if (pc == null) return;

        int touches = touchesByGameObject.Remove(collisionGameObject);

        if (touches < (int) NumberOfPointToCheck)
        {
            Rigidbody r = collisionGameObject.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.isKinematic = false;
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

