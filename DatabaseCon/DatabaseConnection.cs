using System.Data;

namespace DatabaseCon;
using System;
using Npgsql;


    
public class DatabaseConnection
{
    private readonly string _connectionString;
    private  NpgsqlConnection connection;
    public DatabaseConnection()
    {
        _connectionString = "Server=localhost;Port=5432;Database=sep2;User Id=postgres;Password=12344;SearchPath=teachme";
    }
    public NpgsqlConnection connect()
    {
        connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
    public NpgsqlDataReader sql(String query,NpgsqlConnection connection)
    {
        using var command = new NpgsqlCommand(query, connection);
        return command.ExecuteReader();
    }
}