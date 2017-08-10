using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TineController : MonoBehaviour {

    public enum TOUCH_POINTS
    {
        ONE_POINT = 1,
        TWO_POINTS = 2,
        THREE_POINTS = 3,

    }

    private delegate bool IsTouched(GameObject source, GameObject collisionGameObject);

    /// <summary>
    /// Called when all point defined in <see cref="NumberOfPointToCheck"/> has been reached
    /// </summary>
    /// <param name="tineController"></param>
    public event Action<TineController, GameObject> OnTineTouchesCompleted;

    /// <summary>
    /// If all points has been reached and one of them is not collisioning this event will be called
    /// </summary>
    /// <param name="tineController"></param>
    public event Action<TineController, GameObject> OnTineTouchesIncompleted;

    [NonSerialized]
    public TOUCH_POINTS NumberOfPointToCheck;

    private IsTouched fpIsTouched;
    
    private InstanceCounterDictionary<GameObject> touchesByGameObject;

    private void Awake()
    {
        fpIsTouched = SimepleTouch;
        touchesByGameObject = new InstanceCounterDictionary<GameObject>();
    }

    private void OnDestroy()
    {
        touchesByGameObject.Clear();
        touchesByGameObject = null;
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        GameObject pallet = other.transform.parent.gameObject;

        if (fpIsTouched(gameObject, pallet))
        {
            int touches = touchesByGameObject.Add(pallet);

            if (touches == (int)NumberOfPointToCheck)
                if (OnTineTouchesCompleted != null) OnTineTouchesCompleted(this, pallet);
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider other)
    {
        GameObject pallet = other.transform.parent.gameObject;
        int touches = touchesByGameObject.Remove(pallet);
        if (OnTineTouchesIncompleted != null) OnTineTouchesIncompleted(this, pallet);
    }
    
    bool SimepleTouch(GameObject source, GameObject collisionGameObject)
    {
        return true;
    }

}
