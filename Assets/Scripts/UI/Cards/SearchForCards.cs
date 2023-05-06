using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchForCards : MonoBehaviour
{
    public BookLending.CardSearch cardSearch;
    public List<Selected> selected;
    public List<TMP_InputField> inputFields;
    public ShowCardSearchResults showCardSearchResults;
    public void StartSearch()
    {
        cardSearch = new BookLending.CardSearch();
        for (int i = 0; i < selected.Count; i++)
        {
            if (selected[i].selected)
            {
                cardSearch.Flag[i] = true;
            }
            else
            {
                cardSearch.Flag[i] = false;
            }
        }
        cardSearch.CardID = inputFields[0].text;
        cardSearch.Name = inputFields[1].text;
        cardSearch.Department = inputFields[2].text;
        cardSearch.Type = inputFields[3].text;
        showCardSearchResults.cards = BookLending.GetInstance().SearchCards(cardSearch);
        showCardSearchResults.Sort();
        showCardSearchResults.Refresh();
    }
}
