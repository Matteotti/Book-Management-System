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
        private string ID;
        private string Title;
        private string Author;
        private string Publisher;
        private string MinPublishYear;
        private string MaxPublishYear;
        private string MinPrice;
        private string MaxPrice;
        private string Category;
    }

    public List<Book> SearchBook(Search seach)
    {
        //TODO: Search Book
        return null;
    }
    #endregion
}
