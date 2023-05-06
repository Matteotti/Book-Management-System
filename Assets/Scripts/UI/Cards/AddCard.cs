using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddCard : UIClick
{
    public TMP_InputField IDInputField;
    public TMP_InputField nameInputField;
    public TMP_InputField departmentInputField;
    public TMP_InputField typeInputField;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public GameObject errorTipsPrefab, errorTipsPrefabParent;
    public SearchForCards searchForCards;
    public void Add()
    {
        try
        {
            string ID = IDInputField.text;
            string name = nameInputField.text;
            string department = departmentInputField.text;
            string type = typeInputField.text;
            SQLBase.Card card = new SQLBase.Card(ID, name, department, type);
            BookLending.GetInstance().RegisterCard(card);
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
