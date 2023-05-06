using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCardSearchResults : UIClick
{
    public bool isAscending = true;
    public SQLBase.Card.CardInfoType cardInfoType = SQLBase.Card.CardInfoType.CardID;
    public List<SQLBase.Card> cards;
    public GameObject content;
    public void Sort()
    {
        List<SQLBase.Card> temp = new List<SQLBase.Card>();
        temp = BookLending.GetInstance().SortCards(cardInfoType, cards, isAscending);
        cards = temp;
    }
    public void Refresh()
    {
        for(int i = 0;i < Mathf.Min(cards.Count, 30);i++)
        {
            GameObject info = content.transform.GetChild(i + 1).gameObject;
            info.SetActive(true);
            info.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = (i+1).ToString();
            info.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = cards[i].CardId;
            info.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = cards[i].Name;
            info.transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>().text = cards[i].Department;
            info.transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>().text = cards[i].Type;
        }
        for(int i = Mathf.Min(cards.Count, 30);i < 30;i++)
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
