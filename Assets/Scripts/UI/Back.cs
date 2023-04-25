using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : UIClick
{
    public Vector3 previousPosition;
    void Start()
    {
        targetPosition = previousPosition;
    }
    public void Click()
    {
        targetPosition = previousPosition;
        MoveToTarget();
    }
}
