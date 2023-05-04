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
    public class BorrowHistorySearch
    {
        private bool[] _flag = new bool[4];
        public bool[] Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        private string _cardID;
        public string CardID
        {
            get { return _cardID; }
            set { _cardID = value; }
        }
        private string _bookID;
        public string BookID
        {
            get { return _bookID; }
            set { _bookID = value; }
        }
        private string _minBorrowDate;
        public string MinBorrowDate
        {
            get { return _minBorrowDate; }
            set { _minBorrowDate = value; }
        }
        private string _maxBorrowDate;
        public string MaxBorrowDate
        {
            get { return _maxBorrowDate; }
            set { _maxBorrowDate = value; }
        }
        private string _minReturnDate;
        public string MinReturnDate
        {
            get { return _minReturnDate; }
            set { _minReturnDate = value; }
        }
        private string _maxReturnDate;
        public string MaxReturnDate
        {
            get { return _maxReturnDate; }
            set { _maxReturnDate = value; }
        }
    }
    public class CardSearch
    {
        private bool[] _flag = new bool[4];
        public bool[] Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        private string _cardID;
        public string CardID
        {
            get { return _cardID; }
            set { _cardID = value; }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _department;
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
    public void LendBook(Borrow borrow)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "INSERT INTO Borrow (BookID, CardID, BorrowDate, ReturnDate) VALUES ('{0}', '{1}', '{2}', '{3}')";
        command = string.Format(command, borrow.BookID, borrow.CardID, borrow.BorrowDate, "NAN");
        string command2 = "UPDATE Book SET Stock = Stock - 1 WHERE BookID = '{0}'";
        command2 = string.Format(command2, borrow.BookID);
        try
        {
            sqlController.RunNoneQuery(command);
            sqlController.RunNoneQuery(command2);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void ReturnBook(Borrow borrow)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "UPDATE Borrow SET ReturnDate = '{0}' WHERE CardID = '{1}' AND BookID = '{2}' AND BorrowDate = '{3}'";
        command = string.Format(command, borrow.ReturnDate, borrow.CardID, borrow.BookID, borrow.BorrowDate);
        string command2 = "UPDATE Book SET Stock = Stock + 1 WHERE BookID = '{0}'";
        command2 = string.Format(command2, borrow.BookID);
        try
        {
            sqlController.RunNoneQuery(command);
            sqlController.RunNoneQuery(command2);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public List<Borrow> SearchBorrowHistory(BorrowHistorySearch borrowHistorySearch)
    {
        if((borrowHistorySearch.MinReturnDate == "NAN" || borrowHistorySearch.MaxReturnDate == "NAN") && !(borrowHistorySearch.MinReturnDate == "NAN" && borrowHistorySearch.MaxReturnDate == "NAN"))
        {
            return null;
        }
        List<Borrow> borrow = new List<Borrow>();
        bool isFirst = true;
        SQLController sqlController = SQLController.GetInstance();
        string command = "SELECT * FROM Borrow";
        for(int i = 0;i < borrowHistorySearch.Flag.Length;i++)
        {
            if (borrowHistorySearch.Flag[i])
            {
                if (isFirst)
                {
                    command += " WHERE ";
                    isFirst = false;
                }
                else
                {
                    command += " AND ";
                }
                switch (i)
                {
                    case 0:
                        command += "CardID = '" + borrowHistorySearch.CardID + "'";
                        break;
                    case 1:
                        command += "BookID = '" + borrowHistorySearch.BookID + "'";
                        break;
                    case 2:
                        command += "BorrowDate >= '" + borrowHistorySearch.MinBorrowDate + "'";
                        break;
                    case 3:
                        command += "BorrowDate <= '" + borrowHistorySearch.MaxBorrowDate + "'";
                        break;
                    case 4:
                        command += "ReturnDate >= '" + borrowHistorySearch.MinReturnDate + "'";
                        break;
                    case 5:
                        command += "ReturnDate <= '" + borrowHistorySearch.MaxReturnDate + "'";
                        break;
                    default:
                        break;
                }
            }
        }
        command += ";";
        try
        {
            List<object> result = sqlController.SelectMultiData(command);
            foreach (object obj in result)
            {
                borrow.Add((Borrow)obj);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        return borrow;
    }

    public List<Borrow> SortBorrowHistory(Borrow.BorrowInfoType type, List<Borrow> borrow, bool reverse = false)
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
        if (reverse)
        {
            borrow.Reverse();
        }
        return borrow;
    }

    public void RegisterCard(Card card)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "INSERT INTO Card (CardID, CardName, Department, CardType) VALUES ('{0}', '{1}', '{2}', '{3}')";
        command = string.Format(command, card.CardId, card.Name, card.Department, card.Type);
        try
        {
            bool isCardExist = IsCardExist(card.CardId);
            if (isCardExist)
            {
                throw new System.Exception("Card ID is already exist");
            }
            sqlController.RunNoneQuery(command);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void RemoveCard(string cardID)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "DELETE FROM Card WHERE CardID = '{0}'";
        command = string.Format(command, cardID);
        try
        {
            bool isCardBorrowing = IsCardBorrowing(cardID);
            if (isCardBorrowing)
            {
                throw new System.Exception("Card is borrowing book");
            }
            sqlController.RunNoneQuery(command);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private bool IsCardBorrowing(string cardID)
    {
        List<Borrow> borrows = new List<Borrow>();
        SQLController sqlController = SQLController.GetInstance();
        string command = "SELECT * FROM Borrow WHERE CardID = '{0}' AND ReturnDate = 'NAN'";
        command = string.Format(command, cardID);
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                borrows.Add((Borrow)obj);
            }
        }
        catch (System.Exception e)
        {
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
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                cards.Add((Card)obj);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        return cards.Count > 0;
    }

    public List<Card> SearchCards(CardSearch cardSearch)
    {
        List<Card> cards = new List<Card>();
        bool isFirst = true;
        SQLController sqlController = SQLController.GetInstance();
        string command = "SELECT * FROM Card";
        for(int i = 0;i < cardSearch.Flag.Length;i++)
        {
            if (cardSearch.Flag[i])
            {
                if (isFirst)
                {
                    command += " WHERE ";
                    isFirst = false;
                }
                else
                {
                    command += " AND ";
                }
                switch (i)
                {
                    case 0:
                        command += string.Format("CardID = '{0}'", cardSearch.CardID);
                        break;
                    case 1:
                        command += string.Format("CardName = '{0}'", cardSearch.Name);
                        break;
                    case 2:
                        command += string.Format("Department = '{0}'", cardSearch.Department);
                        break;
                    case 3:
                        command += string.Format("CardType = '{0}'", cardSearch.Type);
                        break;
                    default:
                        break;
                }
            }
        }
        command += ";";
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                cards.Add((Card)obj);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        return cards;
    }

    public List<Card> SortCards(Card.CardInfoType type, List<Card> cards, bool reverse = false)
    {
        switch (type)
        {
            case Card.CardInfoType.CardID:
                cards.Sort((x, y) => x.CardId.CompareTo(y.CardId));
                break;
            case Card.CardInfoType.Name:
                cards.Sort((x, y) => x.Name.CompareTo(y.Name) == 0 ? x.CardId.CompareTo(y.CardId) : x.Name.CompareTo(y.Name));
                break;
            case Card.CardInfoType.Department:
                cards.Sort((x, y) => x.Department.CompareTo(y.Department) == 0 ? x.CardId.CompareTo(y.CardId) : x.Department.CompareTo(y.Department));
                break;
            case Card.CardInfoType.Type:
                cards.Sort((x, y) => x.Type.CompareTo(y.Type) == 0 ? x.CardId.CompareTo(y.CardId) : x.Type.CompareTo(y.Type));
                break;
            default:
                break;
        }
        if (reverse)
        {
            cards.Reverse();
        }
        return cards;
    }

    #endregion
}
