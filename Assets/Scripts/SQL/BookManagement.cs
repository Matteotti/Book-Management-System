using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManageMent : SQLBase
{
    #region Singleton
    private static BookManageMent instance;
    public static BookManageMent GetInstance()
    {
        if (instance == null)
        {
            instance = new BookManageMent();
        }
        return instance;
    }
    #endregion

    #region Book Management Methods
    public List<Book> SortBook(Book.BookInfoType queryType, List<Book> books, bool reverse = false)
    {
        switch (queryType)
        {
            case Book.BookInfoType.ID:
                books.Sort((x, y) => x.ID.CompareTo(y.ID));
                break;
            case Book.BookInfoType.Title:
                books.Sort((x, y) => x.Title.CompareTo(y.Title) == 0 ? x.ID.CompareTo(y.ID) : x.Title.CompareTo(y.Title));
                break;
            case Book.BookInfoType.Author:
                books.Sort((x, y) => x.Author.CompareTo(y.Author) == 0 ? x.ID.CompareTo(y.ID) : x.Author.CompareTo(y.Author));
                break;
            case Book.BookInfoType.Publisher:
                books.Sort((x, y) => x.Publisher.CompareTo(y.Publisher) == 0 ? x.ID.CompareTo(y.ID) : x.Publisher.CompareTo(y.Publisher));
                break;
            case Book.BookInfoType.PublishYear:
                books.Sort((x, y) => x.PublishYear.CompareTo(y.PublishYear) == 0 ? x.ID.CompareTo(y.ID) : x.PublishYear.CompareTo(y.PublishYear));
                break;
            case Book.BookInfoType.Price:
                books.Sort((x, y) => x.Price.CompareTo(y.Price) == 0 ? x.ID.CompareTo(y.ID) : x.Price.CompareTo(y.Price));
                break;
            case Book.BookInfoType.Stock:
                books.Sort((x, y) => x.Stock.CompareTo(y.Stock) == 0 ? x.ID.CompareTo(y.ID) : x.Stock.CompareTo(y.Stock));
                break;
            default:
                break;
        }
        if (reverse)
        {
            books.Reverse();
        }
        return books;
    }
    public void StoreBook(Book book)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "INSERT INTO Book (BookID, BookName, Author, Publisher, PublishYear, Price, Stock) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})";
        command = string.Format(command, book.ID, book.Title, book.Author, book.Publisher, book.PublishYear, book.Price, book.Stock);
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            bool isExist = IsBookExist(book);
            if (isExist)
            {
                throw new System.Exception("Book is already exist!");
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
    private bool IsBookExist(Book book)
    {
        List<Book> books = new List<Book>();
        string command = "SELECT * FROM Book WHERE BookID = {0}";
        command = string.Format(command, book.ID);
        SQLController sqlController = SQLController.GetInstance();
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            List<object> objects = sqlController.SelectMultiData(command);
            books = new List<Book>();
            foreach (object obj in objects)
            {
                books.Add((Book)obj);
            }
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
        return books.Count > 0;
    }
    public void StoreBook(List<Book> books)
    {
        SQLController sqlController = SQLController.GetInstance();
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            foreach (Book book in books)
            {
                StoreBook(book);
            }
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
    }

    public void UpdateBookStock(Book book, int deltaStock)
    {
        SQLController sqlController = SQLController.GetInstance();
        if(book.Stock + deltaStock < 0)
        {
            throw new System.Exception("Stock is not enough!");
        }
        else
        {
            string command = "UPDATE Book SET Stock = {0} WHERE BookID = {1}";
            command = string.Format(command, book.Stock + deltaStock, book.ID);
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
    }

    public void RemoveBook(Book book)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "DELETE FROM Book WHERE BookID = {0}";
        command = string.Format(command, book.ID);
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

    public void RemoveBook(List<Book> books)
    {
        SQLController sqlController = SQLController.GetInstance();
        sqlController.Transaction = sqlController.Connect.BeginTransaction();
        try
        {
            foreach (Book book in books)
            {
                RemoveBook(book);
            }
            sqlController.Transaction.Commit();
        }
        catch (System.Exception e)
        {
            sqlController.Transaction.Rollback();
            Debug.Log(e.Message);
        }
    }

    public void ModifyBook(Book book)
    {
        SQLController sqlController = SQLController.GetInstance();
        string command = "UPDATE Book SET BookName = {0}, Author = {1}, Publisher = {2}, PublishYear = {3}, Price = {4}, Stock = {5} WHERE BookID = {6}";
        command = string.Format(command, book.Title, book.Author, book.Publisher, book.PublishYear, book.Price, book.Stock, book.ID);
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
    #endregion
}