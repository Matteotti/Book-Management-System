using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyBookStock : UIClick
{
    public TMP_InputField bookIDInputField;
    public TMP_InputField bookStockInputField;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public GameObject errorTipsPrefab, errorTipsPrefabParent;
    public SearchForBooks searchForBooks;
    public void Modify()
    {
        try
        {
            int bookID = int.Parse(bookIDInputField.text);
            int bookStock = int.Parse(bookStockInputField.text);
            BookManageMent.GetInstance().UpdateBookStock(bookID, bookStock);
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
