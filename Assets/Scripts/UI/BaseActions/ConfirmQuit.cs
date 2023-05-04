using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmQuit : UIClick
{
    public void ConfirmQuitProgram()
    {
        Debug.Log("Quit the program.");
        Application.Quit();
    }
}
