using System.Data.SQLite;

class MessageHistory
{
    public static void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection("Data Source=chat.db"))
        {
            connection.Open();
            string tableCommand = "CREATE TABLE IF NOT EXISTS Messages (Id INTEGER PRIMARY KEY, Username TEXT, Message TEXT)";
            SQLiteCommand createTable = new SQLiteCommand(tableCommand, connection);
            createTable.ExecuteNonQuery();
        }
    }

    public static void SaveMessage(string username, string message)
    {
        using (var connection = new SQLiteConnection("Data Source=chat.db"))
        {
            connection.Open();
            string insertCommand = "INSERT INTO Messages (Username, Message) VALUES (@Username, @Message)";
            SQLiteCommand insertMessage = new SQLiteCommand(insertCommand, connection);
            insertMessage.Parameters.AddWithValue("@Username", username);
            insertMessage.Parameters.AddWithValue("@Message", message);
            insertMessage.ExecuteNonQuery();
        }
    }
}
