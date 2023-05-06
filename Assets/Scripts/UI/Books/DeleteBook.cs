using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeleteBook : UIClick
{
    public TMP_InputField IDInputField;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public GameObject errorTipsPrefab, errorTipsPrefabParent;
    public SearchForBooks searchForBooks;
    public void Delete()
    {
        try
        {
            string ID = IDInputField.text;
            BookManageMent.GetInstance().RemoveBook(ID);
            searchForBooks.StartSearch();
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
