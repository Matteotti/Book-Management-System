using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickMove : MonoBehaviour
{
    public UIMove move = UIMove.instance;
    public Vector3 targetPosition;
    public void MoveToTarget()
    {
        move.Move(targetPosition);
    }
}
