using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSQL : MonoBehaviour
{
    public bool isClear = false;
    void Start()
    {
        SQLController.GetInstance().OpenDataBase();
        if(isClear)
            SQLController.GetInstance().InitializeDataBase();
    }
}
