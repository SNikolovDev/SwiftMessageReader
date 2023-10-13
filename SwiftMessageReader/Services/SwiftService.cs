using System.Security.Cryptography;

using SwiftMessageReader.Data.Interfaces;
using SwiftMessageReader.Exceptions;
using SwiftMessageReader.Helpers;
using SwiftMessageReader.Models;
using SwiftMessageReader.Services.Interfaces;

namespace SwiftMessageReader.Services
{
    public class SwiftService : ISwiftService
    {
        private ISwiftRepository repository;

        public SwiftService(ISwiftRepository repository)
        {
            this.repository = repository;
        }

        public void ManageFile(IFormFile file)
        {
            var text = string.Empty;
            // TODO: Throw an exception if false;
            try
            {
                // Exception thrown from TagVerifier!
                text = FileToStringConverter.ConvertIFormFileToString(file);
                StringToModelParser.CurlyBracketsVerifier(text);
            }
            catch (WrongMessageStructureException)
            {
                throw new ArgumentException(Messages.WrongMessageStructure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var data = StringToDictionaryConverter.ConvertToDictionary(text);

            var model = MessageModelMapper(data);

            repository.InsertIntoDatabase(model);
        }

        private MessageModel MessageModelMapper(Dictionary<int, string> data)
        {
            var model = new MessageModel();

            model.CreatedOn = DateTime.Now;
            model.SendersBankIdentifierCode = data[Keys.SendersBankIdentifierKey];
            model.MessageReferenceNumber = data[Keys.MessageReferenceNumberKey];
            model.TransactionReferenceNumber = data[Keys.TransactionReferenceNumberKey];
            model.ReferenceAssinedByTheSender = data[Keys.ReferenceAssinedByTheSenderKey];
            model.MessageBody = data[Keys.MessageBodyKey];
            model.MessageAuthenticationCode = data[Keys.MessageAuthenticationCodeKey];
            model.CheckValue = data[Keys.CheckValueKey];

            SwiftLogger.Info(Messages.SuccessfulMapping);

            return model;
        }
    }
}
