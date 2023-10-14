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

        public void InsertIntoDatabase(TransferData model)
        {
            var blocks = model.BlocksList;
            var tags = model.TagsList;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertIntoBlcokString = @"
                  INSERT INTO SwiftMessage(
                    CreatedOn,
                    BlockNumber,
                    BlockName,
                    BlockData)

                  VALUES (
                    @CreatedOn,
                    @BlockNumber,
                    @BlockName,                  
                    @BlockData)";

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

                AddBlockParamsWithValues(blocks, insretBlockCmd);
                int swiftMessageId = (int)connection.LastInsertRowId;
                AddTagsParamsWithValues(tags, insertTagsCmd, swiftMessageId);

                SwiftLogger.Info(Messages.SuccessfulDataInsert);

                connection.Close();
            }
        }

        private static void AddBlockParamsWithValues(List<Block> blocks, SQLiteCommand command)
        {
            foreach (var block in blocks)
            {
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@CreatedOn", block.CreatedOn);
                command.Parameters.AddWithValue("@BlockNumber", block.BlockNumber);
                command.Parameters.AddWithValue("@BlockName", block.BlockName);
                command.Parameters.AddWithValue("@BlockData", block.BlockData);

                command.ExecuteNonQuery();
            }
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
