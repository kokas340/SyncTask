namespace Database;

using System;
using Npgsql;


    public class DatabaseConnetion
    {
        private string _connectionString = "Server=localhost;Port=5432;Database=sep2;User Id=postgres;Password=12344;SearchPath=teachme";
        
        private NpgsqlConnection _connection;

        public DatabaseConnetion()
        {
            
            _connection = new NpgsqlConnection(_connectionString);
        }

        public void Connect()
        {
            try
            {
                _connection.Open();
                Console.WriteLine("Connected to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to database: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            try
            {
                _connection.Close();
                Console.WriteLine("Disconnected from database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disconnecting from database: {ex.Message}");
            }
        }

        // Example method for executing a SELECT query
        public void ExecuteSelectQuery(string query)
        {
            try
            {
                using (var cmd = new NpgsqlCommand(query, _connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetString(0)}, {reader.GetString(1)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }

        // Example method for executing a INSERT, UPDATE, or DELETE query
        public void ExecuteNonQuery(string query)
        {
            try
            {
                using (var cmd = new NpgsqlCommand(query, _connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }
    }

