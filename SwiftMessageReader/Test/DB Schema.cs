//using System.Data;
//using System.Data.SQLite;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Data Source=SwiftMessages.db;Version=3;";
//        using (var connection = new SQLiteConnection(connectionString))
//        {
//            connection.Open();

//            using (var command = new SQLiteCommand(connection))
//            {
//                // Create BlockIdentifiersTable
//                command.CommandText = "CREATE TABLE IF NOT EXISTS BlockIdentifiersTable (" +
//                                    "BlockIdentifierId INTEGER PRIMARY KEY AUTOINCREMENT, " +
//                                    "BlockName TEXT, " +
//                                    "Content TEXT, " +
//                                    "SwiftMessageId INTEGER, " +
//                                    "FOREIGN KEY (SwiftMessageId) REFERENCES SwiftMessagesTable(SwiftMessageId));";
//                command.CommandType = CommandType.Text;
//                command.ExecuteNonQuery();

//                // Create TagsTable
//                command.CommandText = "CREATE TABLE IF NOT EXISTS TagsTable (" +
//                                    "TagId INTEGER PRIMARY KEY AUTOINCREMENT, " +
//                                    "TagName TEXT, " +
//                                    "TagContent TEXT, " +
//                                    "SwiftMessageId INTEGER, " +
//                                    "FOREIGN KEY (SwiftMessageId) REFERENCES SwiftMessagesTable(SwiftMessageId));";
//                command.CommandType = CommandType.Text;
//                command.ExecuteNonQuery();

//                // Create Block5Table
//                command.CommandText = "CREATE TABLE IF NOT EXISTS Block5Table (" +
//                                    "Block5Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
//                                    "MessageAuthenticationCode TEXT, " +
//                                    "CheckValue TEXT, " +
//                                    "SwiftMessageId INTEGER, " +
//                                    "FOREIGN KEY (SwiftMessageId) REFERENCES SwiftMessagesTable(SwiftMessageId));";
//                command.CommandType = CommandType.Text;
//                command.ExecuteNonQuery();

//                // Create SwiftMessagesTable
//                command.CommandText = "CREATE TABLE IF NOT EXISTS SwiftMessagesTable (" +
//                                    "SwiftMessageId INTEGER PRIMARY KEY AUTOINCREMENT, " +
//                                    "SwiftMessageName TEXT, " +
//                                    "SwiftMessageContent TEXT);";
//                command.CommandType = CommandType.Text;
//                command.ExecuteNonQuery();
//            }
//        }

//        Console.WriteLine("SQLite database schema created.");
//    }
//}
