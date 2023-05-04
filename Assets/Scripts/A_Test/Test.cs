using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool testMove = false;
    public bool testPop = false;
    public Vector3 pos;
    public UIMove uiMove;
    public UITextPop uiPop;
    void Update()
    {
        if (testMove)
        {
            uiMove.Move(pos);
            testMove = false;
        }
        if (testPop)
        {
            uiPop.Pop(pos);
            testPop = false;
        }
    }
}

//FIXME: Search function
//TODO: Add username and password to login