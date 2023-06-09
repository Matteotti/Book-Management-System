using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQLBase
{
    public class Book
    {
        public enum BookInfoType
        {
            ID,
            Title,
            Author,
            Publisher,
            PublishYear,
            Price,
            Category,
            Stock
        }
        private string _ID = "000000000000";
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _title = "No Title";
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _author = "No Author";
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        private string _publisher = "No Publisher";
        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }
        private string _publishYear = "0000";
        public string PublishYear
        {
            get { return _publishYear; }
            set { _publishYear = value; }
        }
        private double _price = 0.0;
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private string _category = "No Category";
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        private int _stock = 0;
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        public Book(string id, string title, string author, string publisher, string publishYear, double price, string category, int stock)
        {
            _ID = id;
            _title = title;
            _author = author;
            _publisher = publisher;
            _publishYear = publishYear;
            _price = price;
            _category = category;
            _stock = stock;
        }
        public Book()
        {
            
        }
    }
    public class Borrow
    {
        public enum BorrowInfoType
        {
            CardID,
            BookID,
            BorrowDate,
            ReturnDate
        }
        private string _cardID = "000000000000";
        public string CardID
        {
            get { return _cardID; }
            set { _cardID = value; }
        }
        private string _bookID = "000000000000";
        public string BookID
        {
            get { return _bookID; }
            set { _bookID = value; }
        }
        private string _borrowDate = "0000-00-00";
        public string BorrowDate
        {
            get { return _borrowDate; }
            set { _borrowDate = value; }
        }
        private string _returnDate = "NAN";
        public string ReturnDate
        {
            get { return _returnDate; }
            set { _returnDate = value; }
        }
        public Borrow(string cardID, string bookID, string borrowDate, string returnDate)
        {
            _cardID = cardID;
            _bookID = bookID;
            _borrowDate = borrowDate;
            _returnDate = returnDate;
        }
        public Borrow()
        {
            
        }
    }
    public class Card
    {
        public enum CardInfoType
        {
            CardID,
            Name,
            Department,
            Type
        }
        private string _cardId;
        public string CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
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
        public Card(string cardId, string name, string department, string type)
        {
            _cardId = cardId;
            _name = name;
            _department = department;
            _type = type;
        }
    
        public Card()
        {
        }
    }
}
