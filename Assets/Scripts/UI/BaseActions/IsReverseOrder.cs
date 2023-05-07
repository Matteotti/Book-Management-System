using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReverseOrder : UIClick
{
    public enum searchType
    {
        Book,
        Card,
        Borrow
    }
    public searchType type;
    public GameObject descending;
    public GameObject ascending;
    public SQLBase.Book.BookInfoType bookInfoType;
    public SQLBase.Card.CardInfoType cardInfoType;
    public SQLBase.Borrow.BorrowInfoType borrowInfoType;
    public ShowBookSearchResults showBookSearchResults;
    public ShowCardSearchResults showCardSearchResults;
    public ShowBorrowSearchResult ShowBorrowSearchResult;
    public bool isAscending = true;
    void Start()
    {
        if (isAscending)
        {
            descending.SetActive(false);
            ascending.SetActive(true);
        }
        else
        {
            descending.SetActive(true);
            ascending.SetActive(false);
        }
    }
    public void Click()
    {
        if (isAscending)
        {
            isAscending = false;
            descending.SetActive(true);
            ascending.SetActive(false);
        }
        else
        {
            isAscending = true;
            descending.SetActive(false);
            ascending.SetActive(true);
        }
        switch(type)
        {
            case searchType.Book:
                showBookSearchResults.isAscending = isAscending;
                showBookSearchResults.bookInfoType = bookInfoType;
                showBookSearchResults.Sort();
                showBookSearchResults.Refresh();
                break;
            case searchType.Card:
                showCardSearchResults.isAscending = isAscending;
                showCardSearchResults.cardInfoType = cardInfoType;
                showCardSearchResults.Sort();
                showCardSearchResults.Refresh();
                break;
            case searchType.Borrow:
                ShowBorrowSearchResult.isAscending = isAscending;
                ShowBorrowSearchResult.borrowInfoType = borrowInfoType;
                ShowBorrowSearchResult.Sort();
                ShowBorrowSearchResult.Refresh();
                break;
        }
    }
}
