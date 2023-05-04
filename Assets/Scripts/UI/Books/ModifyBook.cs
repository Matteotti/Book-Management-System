using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyBook : UIClick
{
    public TMP_InputField bookIDInputField;
    public TMP_InputField titleInputField;
    public TMP_InputField authorInputField;
    public TMP_InputField publisherInputField;
    public TMP_InputField publishYearInputField;
    public TMP_InputField priceInputField;
    public TMP_InputField categoryInputField;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public GameObject errorTipsPrefab;
    public void Modify()
    {
        try
        {
            string ID = bookIDInputField.text;
            string title = titleInputField.text;
            string author = authorInputField.text;
            string publisher = publisherInputField.text;
            string category = categoryInputField.text;
            string publishYear = publishYearInputField.text;
            float price = float.Parse(priceInputField.text);
            SQLBase.Book book = new SQLBase.Book(ID, title, author, publisher, publishYear, price, category, 0);
            BookManageMent.GetInstance().ModifyBook(book);
        }
        catch
        {
            GameObject errorTips = Instantiate(errorTipsPrefab, Camera.main.WorldToScreenPoint(Vector3.zero) + errorTipsCenterPosition, Quaternion.identity, errorTipsPrefab.transform);
            imagePop = errorTips.transform.GetChild(0).GetComponent<UIImagePop>();
            textPop = errorTips.transform.GetChild(1).GetComponent<UITextPop>();
            targetPosition = deltaPopPosition;
            Pop();
        }
    }
}
