using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowBorrowSearchResult : MonoBehaviour
{
    public bool isAscending = true;
    public SQLBase.Borrow.BorrowInfoType borrowInfoType = SQLBase.Borrow.BorrowInfoType.CardID;
    public List<SQLBase.Borrow> borrowHistories;
    public GameObject content;
    public void Sort()
    {
        List<SQLBase.Borrow> temp = new List<SQLBase.Borrow>();
        temp = BookLending.GetInstance().SortBorrowHistory(borrowInfoType, borrowHistories, isAscending);
        borrowHistories = temp;
    }
    public void Refresh()
    {
        for(int i = 0;i < Mathf.Min(borrowHistories.Count, 50);i++)
        {
            GameObject info = content.transform.GetChild(i + 1).gameObject;
            info.SetActive(true);
            info.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = (i+1).ToString();
            info.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = borrowHistories[i].CardID;
            info.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = borrowHistories[i].BookID;
            info.transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>().text = borrowHistories[i].BorrowDate.ToString();
            info.transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>().text = borrowHistories[i].ReturnDate.ToString();
        }
        for(int i = Mathf.Min(borrowHistories.Count, 50);i < 50;i++)
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