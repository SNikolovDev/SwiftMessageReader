using SwiftMessageReader.Data;
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
            var text = FileToStringConverter.ConvertIFormFileToString(file);
            //using (var reader = new StreamReader(file.OpenReadStream()))
            //{
            //    text = reader.ReadToEnd();
            //}

            var model = new Model();
            var splitedText = text.Split(' ').ToArray();
            model.Name = splitedText[0];
            model.Age = int.Parse(splitedText[1]);

            repository.InsertIntoDatabase(model);
        }
    }
}
