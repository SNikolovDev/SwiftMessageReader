using System.Data.SQLite;

using SwiftMessageReader.Data.Interfaces;
using SwiftMessageReader.Helpers;
using SwiftMessageReader.Models;

namespace SwiftMessageReader.Data
{
    public class SwiftRepository : ISwiftRepository
    {

        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public SwiftRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertIntoDatabase(TransferDataToRepository model)
        {
            var message = model.Model;
            var tags = model.Tags;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertIntoBlcokString = @"
                  INSERT INTO SwiftMessage(
                    CreatedOn,
                    SendersBankIdentifierCode,
                    MessageReferenceNumber,
                    MessageAuthenticationCode,
                    CheckValue)

                  VALUES (
                    @CreatedOn,
                    @SendersBankIdentifierCode,
                    @MessageReferenceNumber,                  
                    @MessageAuthenticationCode,
                    @CheckValue)";

                string insertIntoTagsString = @"
                  INSERT INTO TagsInformationTable(
                    SwiftMessageId,
                    CreatedOn,
                    TagNumber,
                    TagName,
                    TagData)

                   VALUES (
                     @SwiftMessageId,
                     @CreatedOn,
                     @TagNumber,
                     @TagName,
                     @TagData)";

                var insretBlockCmd = new SQLiteCommand(insertIntoBlcokString, connection);
                var insertTagsCmd = new SQLiteCommand(insertIntoTagsString, connection);

                AddBlockParamsWithValues(message, insretBlockCmd);
                insretBlockCmd.ExecuteNonQuery();
                int swiftMessageId = (int)connection.LastInsertRowId;

                AddTagsParamsWithValues(tags, insertTagsCmd, swiftMessageId);

                SwiftLogger.Info(Messages.SuccessfulDataInsert);

                connection.Close();
            }
        }

        private static void AddBlockParamsWithValues(Blocks model, SQLiteCommand command)
        {
            command.Parameters.AddWithValue("@CreatedOn", model.CreatedOn);
            command.Parameters.AddWithValue("@SendersBankIdentifierCode", model.SendersBankIdentifierCode);
            command.Parameters.AddWithValue("@MessageReferenceNumber", model.MessageReferenceNumber);
            command.Parameters.AddWithValue("@MessageAuthenticationCode", model.MessageAuthenticationCode);
            command.Parameters.AddWithValue("@CheckValue", model.CheckValue);
        }
        private static void AddTagsParamsWithValues(List<Tag> tags, SQLiteCommand command, int swiftMessageId)
        {
            foreach (var tag in tags)
            {
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@SwiftMessageId", swiftMessageId);
                command.Parameters.AddWithValue("@CreatedOn", tag.CreatedOn);
                command.Parameters.AddWithValue("@TagNumber", tag.TagNumber);
                command.Parameters.AddWithValue("@TagName", tag.TagName);
                command.Parameters.AddWithValue("@TagData", tag.TagData);

                command.ExecuteNonQuery();
            }
        }
    }
}
