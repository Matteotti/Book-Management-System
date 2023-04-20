using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
public class SQLController
{
    #region Private Variables
    string sqlServer = "127.0.0.1";
    string sqlDatabase = "EXP05";
    string uid = "sa";
    string pwd = "030811";
    string sqlConnectionInfo;
    private SqlConnection _connect;
    public SqlConnection Connect
    {
        get
        {
            return _connect;
        }
        set
        {
            _connect = value;
        }
    }
    private SqlCommand command;
    private SqlDataReader reader;
    private SqlTransaction _transaction;
    public SqlTransaction Transaction
    {
        get
        {
            return _transaction;
        }
        set
        {
            _transaction = value;
        }
    }
    #endregion

    #region Sinleton
    private static SQLController instance;
    public static SQLController GetInstance()
    {
        if (instance == null)
        {
            instance = new SQLController();
        }
        return instance;
    }
    private SQLController()
    {
    }
    #endregion

    #region Base Methods
    public void OpenDataBase()
    {
        sqlConnectionInfo = "Data Source=" + sqlServer + ";Initial Catalog=" + sqlDatabase + ";User ID=" + uid + ";Password=" + pwd;
    	_connect = new SqlConnection(sqlConnectionInfo);
    	try
        {
            _connect.Open();
            command = _connect.CreateCommand();
        }
        catch (SqlException e)
        {
            Debug.LogError(e.Message);
            
        }
    }  

    public void CloseDataBase()
    {
        try
        {
            _connect.Close();
        }
        catch (SqlException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void RunNoneQuery(string sql)
    {
        try
        {
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
        catch (SqlException e)
        {
            Debug.Log(e.Message);
        }
    }

    public object SelectSingleData(string sql)
    {
        object result = null;
        try
        {
            command.CommandText = sql;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                result = reader[0];
            }
            reader.Close();
        }
        catch (SqlException e)
        {
            Debug.Log(e.Message);
        }
        return result;
    }

    public List<object> SelectMultiData(string sql)
    {
        List<object> result = new List<object>();
        try
        {
            command.CommandText = sql;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader[0]);
            }
            reader.Close();
        }
        catch (SqlException e)
        {
            Debug.Log(e.Message);
        }
        return result;
    }

    public void InitializeDataBase()
    {
        OpenDataBase();
        RunNoneQuery("DROP TABLE IF EXISTS 'Book'");
        RunNoneQuery("DROP TABLE IF EXISTS 'Borrow'");
        RunNoneQuery("DROP TABLE IF EXISTS 'Card'");
        RunNoneQuery("CREATE TABLE Book (BookID VARCHAR(20), BookName VARCHAR(20), Author VARCHAR(20), Publisher VARCHAR(20), PublishYear VARCHAR(20), Price decimal(7, 2), Category VARCHAR(20), Stock int,"
                   + "primary key (BookID)), unique(Category, Publisher, PublishYear, Author, BookName))"
                   + "engine = innodb default charset = utf8");
        RunNoneQuery("CREATE TABLE Borrow (BookID VARCHAR(20), CardID VARCHAR(20), BorrowDate VARCHAR(20), ReturnDate VARCHAR(20),"
                   + "FOREIGN KEY (BookID) REFERENCES Book(BookID), FOREIGN KEY (CardID) REFERENCES Card(CardID))"
                   + "engine = innodb default charset = utf8");
        RunNoneQuery("CREATE TABLE Card (CardID VARCHAR(20), CardName VARCHAR(20), CardType VARCHAR(20), Department VARCHAR(20),"
                   + "primary key ('CardID'), unique('CardName', 'CardType', 'Department'))"
                   + "engine = innodb default charset = utf8");
    }
    #endregion 
}