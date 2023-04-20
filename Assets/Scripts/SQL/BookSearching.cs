using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSearching : SQLBase
{
    #region Singleton
    private static BookSearching instance;
    public static BookSearching GetInstance()
    {
        if (instance == null)
        {
            instance = new BookSearching();
        }
        return instance;
    }
    #endregion

    #region Book Searching Methods

    public class Search
    {
        private bool[] _flag = new bool[7];
        public bool[] Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        private string _ID;
        public string ID
        {
            get { return ID; }
            set { ID = value; }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _author;
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        private string _publisher;
        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }
        private string _minPublishYear;
        public string MinPublishYear
        {
            get { return _minPublishYear; }
            set { _minPublishYear = value; }
        }
        private string _maxPublishYear;
        public string MaxPublishYear
        {
            get { return _maxPublishYear; }
            set { _maxPublishYear = value; }
        }
        private string _minPrice;
        public string MinPrice
        {
            get { return _minPrice; }
            set { _minPrice = value; }
        }
        private string _maxPrice;
        public string MaxPrice
        {
            get { return _maxPrice; }
            set { _maxPrice = value; }
        }
        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
    }

    public List<Book> SearchBook(Search search)
    {
        List<Book> books = new List<Book>();
        bool isFirst = true;
        string command = "SELECT * FROM BookID WHERE";
        SQLController sqlController = SQLController.GetInstance();
        for(int i = 0; i < search.Flag.Length; i++)
        {
            if (search.Flag[i])
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    command += " AND";
                }
                switch (i)
                {
                    case 0:
                        command += " BookID LIKE " + search.ID;
                        break;
                    case 1:
                        command += " BookName LIKE " + search.Title;
                        break;
                    case 2:
                        command += " Author LIKE " + search.Author;
                        break;
                    case 3:
                        command += " Publisher LIKE " + search.Publisher;
                        break;
                    case 4:
                        command += " PublishYear BETWEEN " + search.MinPublishYear.ToString() + " AND " + search.MaxPublishYear.ToString();
                        break;
                    case 5:
                        command += " Price BETWEEN " + search.MinPrice.ToString() + " AND " + search.MaxPrice.ToString();
                        break;
                    case 6:
                        command += " Category LIKE " + search.Category;
                        break;
                    default:
                        break;
                }
            }
        }
        command += ";";
        if(isFirst)
        {
            return books;
        }
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            foreach (object obj in objects)
            {
                books.Add((Book)obj);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        return books;
    }
    #endregion
}