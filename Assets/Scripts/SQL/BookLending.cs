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
        SQLController sqlController = SQLController.GetInstance();
        string command = "INSERT INTO Borrow (BookID, CardID, BorrowDate, ReturnDate) VALUES ('{0}', '{1}', '{2}', '{3}')";
        command = string.Format(command, borrow.BookID, borrow.CardID, borrow.BorrowDate, "NAN");
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            sqlController.RunNoneQuery(command);
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
    }

    public void ReturnBook(Borrow borrow)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "UPDATE Borrow SET ReturnDate = '{0}' WHERE CardID = '{1}' AND BookID = '{2}' AND BorrowDate = '{3}'";
        command = string.Format(command, borrow.ReturnDate, borrow.CardID, borrow.BookID, borrow.BorrowDate);
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            sqlController.RunNoneQuery(command);
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
    }

    public List<Borrow> ShowBorrowHistory(string cardID)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "SELECT * FROM Borrow WHERE CardID = '{0}'";
        command = string.Format(command, cardID);
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        List<Borrow> borrow = new List<Borrow>();
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                borrow.Add((Borrow)obj);
            }
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
        return borrow;
    }

    public List<Borrow> SortBorrowHistory(Borrow.BorrowInfoType type, List<Borrow> borrow)
    {
        switch (type)
        {
            case Borrow.BorrowInfoType.CardID:
                borrow.Sort((x, y) => x.CardID.CompareTo(y.CardID));
                break;
            case Borrow.BorrowInfoType.BookID:
                borrow.Sort((x, y) => x.BookID.CompareTo(y.BookID) == 0 ? x.CardID.CompareTo(y.CardID) : x.BookID.CompareTo(y.BookID));
                break;
            case Borrow.BorrowInfoType.BorrowDate:
                borrow.Sort((x, y) => x.BorrowDate.CompareTo(y.BorrowDate) == 0 ? x.CardID.CompareTo(y.CardID) : x.BorrowDate.CompareTo(y.BorrowDate));
                break;
            case Borrow.BorrowInfoType.ReturnDate:
                borrow.Sort((x, y) => x.ReturnDate.CompareTo(y.ReturnDate) == 0 ? x.CardID.CompareTo(y.CardID) : x.ReturnDate.CompareTo(y.ReturnDate));
                break;
            default:
                break;
        }
        return borrow;
    }

    public void RegisterCard(Card card)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "INSERT INTO Card (CardID, CardName, Department, CardType) VALUES ('{0}', '{1}', '{2}', '{3}')";
        command = string.Format(command, card.CardId, card.Name, card.Department, card.Type);
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            bool isCardExist = IsCardExist(card.CardId);
            if (isCardExist)
            {
                throw new System.Exception("Card ID is already exist");
            }
            sqlController.RunNoneQuery(command);
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
    }

    public void RemoveCard(string cardID)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "DELETE FROM Card WHERE CardID = '{0}'";
        command = string.Format(command, cardID);
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            bool isCardBorrowing = IsCardBorrowing(cardID);
            if (isCardBorrowing)
            {
                throw new System.Exception("Card is borrowing book");
            }
            sqlController.RunNoneQuery(command);
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
    }

    private bool IsCardBorrowing(string cardID)
    {
        List<Borrow> borrows = new List<Borrow>();
        SQLController sqlController = SQLController.GetInstance();
        string command = "SELECT * FROM Borrow WHERE CardID = '{0}' AND ReturnDate = 'NAN'";
        command = string.Format(command, cardID);
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                borrows.Add((Borrow)obj);
            }
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
        return borrows.Count > 0;
    }

    private bool IsCardExist(string cardID)
    {
        List<Card> cards = new List<Card>();
        SQLController sqlController = SQLController.GetInstance();
        string command = "SELECT * FROM Card WHERE CardID = '{0}'";
        command = string.Format(command, cardID);
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                cards.Add((Card)obj);
            }
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
        return cards.Count > 0;
    }

    public List<Card> ShowCards()
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "SELECT * FROM Card";
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        List<Card> cards = new List<Card>();
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                cards.Add((Card)obj);
            }
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
        cards.Sort((x, y) => x.CardId.CompareTo(y.CardId));
        return cards;
    }
    #endregion
}
