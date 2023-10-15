using System.Data.SQLite;

using SwiftMessageReader.Data.Interfaces;
using SwiftMessageReader.Helpers;
using SwiftMessageReader.Models;

namespace SwiftMessageReader.Data
{
    public class SwiftRepository : ISwiftRepository
    {
        private readonly string connectionString;

        public SwiftRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertIntoDatabase(TransferData model)
        {
            var blocks = model.Blocks;
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
                    TransactionReferenceNumberTag,
                    TransactionReferenceNumber,
                    ReferenceAssinedByTheSenderTag,
                    ReferenceAssinedByTheSender,
                    MessageBodyTag,
                    MessageBody)

                   VALUES (
                     @SwiftMessageId,
                     @CreatedOn,
                     @TransactionReferenceNumberTag,
                     @TransactionReferenceNumber,
                     @ReferenceAssinedByTheSenderTag,
                     @ReferenceAssinedByTheSender,
                     @MessageBodyTag,
                     @MessageBody)";

                var insertBlockCmd = new SQLiteCommand(insertIntoBlcokString, connection);
                var insertTagsCmd = new SQLiteCommand(insertIntoTagsString, connection);

                AddBlockParamsWithValues(blocks, insertBlockCmd);
                insertBlockCmd.ExecuteNonQuery();

                int swiftMessageId = (int)connection.LastInsertRowId;

                AddTagParamsWithValues(tags, insertTagsCmd, swiftMessageId);
                insertTagsCmd.ExecuteNonQuery();

                SwiftLogger.Info(Messages.SuccessfulDataInsert);
                connection.Close();
            }
        }

        private static void AddBlockParamsWithValues(Blocks blocks, SQLiteCommand command)
        {
            command.Parameters.AddWithValue("@CreatedOn", blocks.CreatedOn);
            command.Parameters.AddWithValue("@SendersBankIdentifierCode", blocks.SendersBankIdentifierCode);
            command.Parameters.AddWithValue("@MessageReferenceNumber", blocks.MessageReferenceNumber);
            command.Parameters.AddWithValue("@MessageAuthenticationCode", blocks.MessageAuthenticationCode);
            command.Parameters.AddWithValue("@CheckValue", blocks.CheckValue);
        }

        private static void AddTagParamsWithValues(Tags tags, SQLiteCommand command, int swiftMessageId)
        {
            command.Parameters.AddWithValue("@SwiftMessageId", swiftMessageId);
            command.Parameters.AddWithValue("@CreatedOn", tags.CreatedOn);
            command.Parameters.AddWithValue("@TransactionReferenceNumberTag", tags.TransactionReferenceNumberTag);
            command.Parameters.AddWithValue("@TransactionReferenceNumber", tags.TransactionReferenceNumber);
            command.Parameters.AddWithValue("@ReferenceAssinedByTheSenderTag", tags.ReferenceAssinedByTheSenderTag);
            command.Parameters.AddWithValue("@ReferenceAssinedByTheSender", tags.ReferenceAssinedByTheSender);
            command.Parameters.AddWithValue("@MessageBodyTag", tags.MessageBodyTag);
            command.Parameters.AddWithValue("@MessageBody", tags.MessageBody);
        }
    }
}