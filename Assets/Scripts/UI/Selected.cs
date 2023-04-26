using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected : UIClick
{
    public Sprite selectedSprite;
    public Sprite unselectedSprite;
    public bool selected = false;
    void DoSelected()
    {
        selected = true;
        GetComponent<Image>().sprite = selectedSprite;
    }
    void Unselected()
    {
        selected = false;
        GetComponent<Image>().sprite = unselectedSprite;
    }
    public void Click()
    {
        if (selected)
        {
            Unselected();
        }
        else
        {
            DoSelected();
        }
    }
}
