using SwiftMessageReader.Data.Interfaces;
using SwiftMessageReader.Exceptions;
using SwiftMessageReader.Helpers;
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
            var fileAsString = string.Empty;
            // TODO: Throw an exception if false;
            try
            {
                // Exception thrown from TagVerifier!
                fileAsString = FileToStringConverter.ConvertIFormFileToString(file);
                MessageStructureVerifiers.CurlyBracketsVerifier(fileAsString);
            }
            catch (WrongMessageStructureException)
            {
                throw new ArgumentException(Messages.WrongMessageStructure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); // TODO: Exception;
            }

            var blocksAndTags = Parser.SplitByBlocksAndTags(fileAsString);
            repository.InsertIntoDatabase(blocksAndTags);
        }
    }
}
