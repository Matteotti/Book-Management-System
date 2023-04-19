using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookLending : SQLBase
{
    #region Singleton
    private static BookLending instance;
    public static BookLending GetInstance()
    {
        if (instance == null)
        {
            instance = new BookLending();
        }
        return instance;
    }
    #endregion

    #region Book Lending Methods
    public void LendBook(Borrow borrow)
    {
        //TODO: Lend Book
    }

    public void ReturnBook(Borrow borrow)
    {
        //TODO: Return Book
    }

    public List<Borrow> ShowBorrowHistory(string cardID)
    {
        //TODO: Show Borrow History
        return null;
    }

    public void RegisterCard(Card card)
    {
        //TODO: Register Card
    }

    public void RemoveCard(string cardID)
    {
        //TODO: Remove Card
    }

    public List<Card> ShowCards()
    {
        //TODO: Show Cards
        return null;
    }
    #endregion
}
