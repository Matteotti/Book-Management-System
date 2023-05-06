using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyCard : UIClick
{
    public TMP_InputField cardIDInputField;
    public TMP_InputField cardNameInputField;
    public TMP_InputField cardDepartmentInputField;
    public TMP_InputField cardTypeInputField;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public GameObject errorTipsPrefab, errorTipsPrefabParent;
    public SearchForCards searchForCards;
    public void Modify()
    {
        try
        {
            string ID = cardIDInputField.text;
            string name = cardNameInputField.text;
            string department = cardDepartmentInputField.text;
            string type = cardTypeInputField.text;
            SQLBase.Card card = new SQLBase.Card(ID, name, department, type);
            BookLending.GetInstance().ModifyCard(card);
            searchForCards.StartSearch();
        }
        catch
        {
            GameObject errorTips = Instantiate(errorTipsPrefab, Camera.main.WorldToScreenPoint(Vector3.zero) + errorTipsCenterPosition, Quaternion.identity, errorTipsPrefabParent.transform);
            imagePop = errorTips.transform.GetChild(0).GetComponent<UIImagePop>();
            textPop = errorTips.transform.GetChild(1).GetComponent<UITextPop>();
            targetPosition = deltaPopPosition;
            Pop();
        }
    }
}
