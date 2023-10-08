using SwiftMessageReader.Data;
using SwiftMessageReader.Helpers;
using SwiftMessageReader.Models;

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
            var text = FileToStringConverter.ConvertIFormFileToString(file).ToString();
            //using (var reader = new StreamReader(file.OpenReadStream()))
            //{
            //    text = reader.ReadToEnd();
            //}

            var model = new MessageModel();
            StringToDictionaryConverter.ConvertToDictionary(text);



            ;
         

            repository.InsertIntoDatabase(model);
        }
    }
}
