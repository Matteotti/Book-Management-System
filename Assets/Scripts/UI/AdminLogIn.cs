using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdminLogIn : UIClick
{
    public Vector3 adminChoosingWindowCenterPosition;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public string adminUsername;
    public string adminPassword;
    public GameObject errorTipsPrefab, errorTipsPrefabParent;
    public void CheckLogIn()
    {
        if (usernameInputField.text == adminUsername && passwordInputField.text == adminPassword)
        {
            targetPosition = adminChoosingWindowCenterPosition;
            MoveToTarget();
        }
        else
        {
            GameObject errorTips = Instantiate(errorTipsPrefab, Camera.main.WorldToScreenPoint(Vector3.zero) + errorTipsCenterPosition, Quaternion.identity, errorTipsPrefabParent.transform);
            imagePop = errorTips.transform.GetChild(0).GetComponent<UIImagePop>();
            textPop = errorTips.transform.GetChild(1).GetComponent<UITextPop>();
            targetPosition = deltaPopPosition;
            Pop();
        }
    }
}