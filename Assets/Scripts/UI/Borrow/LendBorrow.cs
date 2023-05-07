using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LendBorrow : UIClick
{
    public TMP_InputField cardIDInputField;
    public TMP_InputField bookIDInputField;
    public TMP_InputField borrowDateInputField;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public GameObject errorTipsPrefab, errorTipsPrefabParent;
    public SearchForBorrow searchForBorrow;
    public void Lend()
    {
        try
        {
            string cardID = cardIDInputField.text;
            string bookID = bookIDInputField.text;
            string borrowDate = borrowDateInputField.text;
            string returnDate = "NAN";
            SQLBase.Borrow borrow = new SQLBase.Borrow(cardID, bookID, borrowDate, returnDate);
            BookLending.GetInstance().LendBook(borrow);
            searchForBorrow.StartSearch();
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
