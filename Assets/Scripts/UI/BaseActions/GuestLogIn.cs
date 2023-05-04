using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestLogIn : UIClick
{
    public Vector3 GuestSearchingWindowCenterPosition;
    void Start()
    {
        targetPosition = GuestSearchingWindowCenterPosition;
    }
    public void Click()
    {
        targetPosition = GuestSearchingWindowCenterPosition;
        MoveToTarget();
    }
}