using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : UIClick
{
    public GameObject infoWindow;
    public GameObject closeButtonOfInfoWindow;
    public Vector3 endPos;
    public void ShowInfoWindow()
    {
        //TODO: 窗口出现之后禁用点击
        shows = new List<UIShow>();
        shows.Add(infoWindow.GetComponent<UIShow>());
        closeButtonOfInfoWindow.GetComponent<CloseWindow>().button = gameObject;
        startPosition = transform.position;
        targetPosition = endPos;
        Appear();
    }
}