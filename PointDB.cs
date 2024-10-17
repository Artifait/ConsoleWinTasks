﻿using QuizTop.UI;
using System.Data;
using Microsoft.Data.SqlClient;

public class PointDB
{
    SqlConnection conn;
    public PointDB(string database, string server = @"(localdb)\MSSQLLocalDB")
    {
        string conStr = $@"Server={server};
                        Database={database};
                        Trusted_Connection=True;";
        conn = new SqlConnection(conStr);
    }

    public bool OpenDB()
    {
        if (IsWork()) return true;
        try
        {
            conn.Open();
            if (conn.State == ConnectionState.Open)
                return true;
        }
        catch (Exception ex)
        {
            WindowsHandler.AddErroreWindow([ex.Message], true);
        }
        return false;
    }

    public void CloseDB()
    {
        if (conn.State == ConnectionState.Open)
            conn.Close();
    }

    public bool IsWork() => conn.State == ConnectionState.Open;

    private DataTable ExecuteQuery(string query)
    {
        DataTable dt = new();
        try
        {
            if (OpenDB())
            {
                using SqlCommand cmd = new(query, conn);
                using SqlDataAdapter adapter = new(cmd);
                adapter.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CloseDB();
            WindowsHandler.AddErroreWindow(["Сломалась бд :(", ex.Message], true);
        }
        return dt;
    }
}