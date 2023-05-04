using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowBookSearchResults : UIClick
{
    public bool isAscending = true;
    public SQLBase.Book.BookInfoType bookInfoType = SQLBase.Book.BookInfoType.ID;
    public List<SQLBase.Book> books;
    public GameObject content;
    public void Sort()
    {
        List<SQLBase.Book> temp = new List<SQLBase.Book>();
        temp = BookSearching.GetInstance().SortBook(bookInfoType, books, isAscending);
        books = temp;
    }
    public void Refresh()
    {
        for(int i = 0;i < Mathf.Min(books.Count, 50);i++)
        {
            GameObject info = content.transform.GetChild(i + 1).gameObject;
            info.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = (i+1).ToString();
            info.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = books[i].ID;
            info.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = books[i].Title;
            info.transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>().text = books[i].Author;
            info.transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>().text = books[i].Category;
            info.transform.GetChild(5).GetChild(1).GetComponent<TMP_Text>().text = books[i].Publisher;
            info.transform.GetChild(6).GetChild(1).GetComponent<TMP_Text>().text = books[i].PublishYear.ToString();
            info.transform.GetChild(7).GetChild(1).GetComponent<TMP_Text>().text = books[i].Price.ToString();
            info.transform.GetChild(8).GetChild(1).GetComponent<TMP_Text>().text = books[i].Stock.ToString();
        }
        for(int i = Mathf.Min(books.Count, 50);i < 50;i++)
        {
            GameObject info = content.transform.GetChild(i + 1).gameObject;
            info.SetActive(false);
        }
    }
    private void Start()
    {
        Sort();
        Refresh();
    }
}