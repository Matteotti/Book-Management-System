using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchForBorrow : MonoBehaviour
{
    public BookLending.BorrowHistorySearch searchInfo;
    public List<Selected> selected;
    public List<TMP_InputField> inputFields;
    public ShowBorrowSearchResult showBorrowSearchResult;
    public void StartSearch()
    {
        searchInfo = new BookLending.BorrowHistorySearch();
        for (int i = 0; i < selected.Count; i++)
        {
            if (selected[i].selected)
            {
                searchInfo.Flag[i] = true;
            }
            else
            {
                searchInfo.Flag[i] = false;
            }
        }
        searchInfo.CardID = inputFields[0].text;
        searchInfo.BookID = inputFields[1].text;
        searchInfo.MinBorrowDate = inputFields[2].text;
        searchInfo.MaxBorrowDate = inputFields[3].text;
        searchInfo.MinReturnDate = inputFields[4].text;
        searchInfo.MaxReturnDate = inputFields[5].text;
        showBorrowSearchResult.borrowHistories = BookLending.GetInstance().SearchBorrowHistory(searchInfo);
        showBorrowSearchResult.Sort();
        showBorrowSearchResult.Refresh();
    }
}
