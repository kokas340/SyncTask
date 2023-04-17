namespace Database;

public class DatabaseTest
{
    public static void Main(string[] args)
    {
        var connector = new DatabaseConnetion();
        connector.Connect();

// Execute a SELECT query
        connector.ExecuteSelectQuery("SELECT * FROM admin");

// Execute an INSERT query
    

        connector.Disconnect();
    }
  
}