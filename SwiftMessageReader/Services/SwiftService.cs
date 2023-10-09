using SwiftMessageReader.Data.Interfaces;
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
            var text = FileToStringConverter.ConvertIFormFileToString(file);         
            var data = StringToDictionaryConverter.ConvertToDictionary(text);

            var model = MessageModelMapper(data);
            
            repository.InsertIntoDatabase(model);
        }

        private MessageModel MessageModelMapper(Dictionary<int, string> data)
        {
            var model = new MessageModel();

            model.CreatedOn = DateTime.Now;
            model.SendersBankIdentifierCode = data[1];
            model.MessageReferenceNumber = data[2];
            model.TransactionReferenceNumber = data[3];
            model.ReferenceAssinedByTheSender = data[4];
            model.MessageBody = data[5];
            model.MessageAuthenticationCode = data[6];
            model.CheckValue = data[7];

            SwiftLogger.Info(NLogMessages.SuccessfulMapping);

            return model;
        }
    }
}
