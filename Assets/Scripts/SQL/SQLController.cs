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

    public DataSet SelectData(string sql)
    {
        command.CommandText = sql;
        SqlDataAdapter da = new SqlDataAdapter(command);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public void InitializeDataBase()
    {
        OpenDataBase();
        RunNoneQuery("DROP TABLE Borrow");
        RunNoneQuery("DROP TABLE Card");
        RunNoneQuery("DROP TABLE Book");
        RunNoneQuery("CREATE TABLE Book (BookID VARCHAR(50), BookName VARCHAR(50), Author VARCHAR(50), Publisher VARCHAR(50), PublishYear VARCHAR(50), Price decimal(7, 2), Category VARCHAR(50), Stock int, primary key (BookID), unique(BookName, Author, Publisher, PublishYear))");
        RunNoneQuery("CREATE TABLE Card (CardID VARCHAR(50), CardName VARCHAR(50), CardType VARCHAR(50), Department VARCHAR(50), primary key (CardID), unique(CardName, CardType, Department))");
        RunNoneQuery("CREATE TABLE Borrow (BookID VARCHAR(50), CardID VARCHAR(50), BorrowDate VARCHAR(50), ReturnDate VARCHAR(50), FOREIGN KEY (BookID) REFERENCES Book(BookID), FOREIGN KEY (CardID) REFERENCES Card(CardID))");
    }
    #endregion 
}