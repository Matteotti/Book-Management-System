using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : UIClick
{
    public GameObject window;
    public GameObject button, backupButton;
    public void Close()
    {
        if(backupButton != null)
            button = backupButton.GetComponent<CloseWindow>().button;
        shows = new List<UIShow>();
        shows.Add(window.GetComponent<UIShow>());
        startPosition = transform.position;
        targetPosition = button.transform.position;
        Disappear();
    }
}