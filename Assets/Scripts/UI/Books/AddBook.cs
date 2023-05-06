using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddBook : UIClick
{
    public TMP_InputField IDInputField;
    public TMP_InputField titleInputField;
    public TMP_InputField authorInputField;
    public TMP_InputField publisherInputField;
    public TMP_InputField publishYearInputField;
    public TMP_InputField priceInputField;
    public TMP_InputField categoryInputField;
    public TMP_InputField stockInputField;
    public Vector3 errorTipsCenterPosition;
    public Vector3 deltaPopPosition;
    public GameObject errorTipsPrefab, errorTipsPrefabParent;
    public SearchForBooks searchForBooks;
    public void Add()
    {
        try
        {
            string ID = IDInputField.text;
            string title = titleInputField.text;
            string author = authorInputField.text;
            string publisher = publisherInputField.text;
            string publishYear = publishYearInputField.text;
            float price = float.Parse(priceInputField.text);
            string category = categoryInputField.text;
            int stock = int.Parse(stockInputField.text);
            SQLBase.Book book = new SQLBase.Book(ID, title, author, publisher, publishYear, price, category, stock);
            BookManageMent.GetInstance().StoreBook(book);
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