using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickPop : MonoBehaviour
{
    public UITextPop textPop;
    public UIImagePop imagePop;
    public Vector3 targetPosition;
    public void Pop()
    {
        textPop.Pop(targetPosition);
        imagePop.Pop(targetPosition);
    }
}
