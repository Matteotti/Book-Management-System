using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitProgram : UIClick
{
    public GameObject confirmWindow;
    public GameObject closeButtonOfConfirmWindow;
    public Vector3 endPos;
    public void ShowQuitWindow()
    {
        //TODO: 窗口出现之后禁用点击
        shows = new List<UIShow>();
        shows.Add(confirmWindow.GetComponent<UIShow>());
        closeButtonOfConfirmWindow.GetComponent<CloseWindow>().button = gameObject;
        startPosition = transform.position;
        targetPosition = endPos;
        Appear();
    }
}