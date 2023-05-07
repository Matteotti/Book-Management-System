using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchForBooks : UIClick
{
    public BookSearching.Search searchInfo;
    public List<Selected> selected;
    public List<TMP_InputField> inputFields;
    public ShowBookSearchResults showBookSearchResults;
    public void StartSearch()
    {
        searchInfo = new BookSearching.Search();
        for(int i = 0;i < selected.Count;i++)
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
        searchInfo.ID = inputFields[0].text;
        searchInfo.Title = inputFields[1].text;
        searchInfo.Author = inputFields[2].text;
        searchInfo.Category = inputFields[3].text;
        searchInfo.Publisher = inputFields[4].text;
        searchInfo.MinPublishYear = inputFields[5].text;
        searchInfo.MaxPublishYear = inputFields[6].text;
        searchInfo.MinPrice = inputFields[7].text;
        searchInfo.MaxPrice = inputFields[8].text;
        showBookSearchResults.books = BookSearching.GetInstance().SearchBook(searchInfo);
        showBookSearchResults.Sort();
        showBookSearchResults.Refresh();
    }
}
