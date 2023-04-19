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
    public List<Book> QueryBook(Book.BookInfoType queryType, List<Book> books)
    {
        //TODO: Query Book
        return null;
    }
    public void StoreBook(Book book)
    {
        //TODO: Store Book
    }

    public void StoreBook(List<Book> books)
    {
        //TODO: Store Books
    }

    public void UpdateBookStock(Book book, int stock)
    {
        //TODO: Update Book Stock
    }

    public void RemoveBook(Book book)
    {
        //TODO: Remove Book
    }

    public void RemoveBook(List<Book> books)
    {
        //TODO: Remove Books
    }

    public void ModifyBook(Book book)
    {
        //TODO: Modify Book
    }
    #endregion

}