using SwiftMessageReader.Data.Interfaces;
using SwiftMessageReader.Exceptions;
using SwiftMessageReader.Helpers;
using SwiftMessageReader.Models;
using SwiftMessageReader.Services.Interfaces;

namespace SwiftMessageReader.Services
{
    public class SwiftService : ISwiftService
    {
        private readonly ISwiftRepository repository;

        public SwiftService(ISwiftRepository repository)
        {
            this.repository = repository;
        }

        public void ManageFile(IFormFile file)
        {
            TransferData blocksAndTags;

            try
            {
                var fileAsString = FileToStringConverter.ConvertIFormFileToString(file);
                MessageStructureVerifiers.CurlyBracketsVerifier(fileAsString);
                blocksAndTags = Parser.SplitByBlocksAndTags(fileAsString);
            }
            catch (InvalidFileException)
            {
                SwiftLogger.Error(Messages.InvalidFileExceptionMessage);
                throw new InvalidFileException(Messages.InvalidFileExceptionMessage);
            }
            catch (WrongBracketsSequence)
            {
                SwiftLogger.Error(Messages.WrongBracketsSequence);
                throw new WrongBracketsSequence(Messages.WrongBracketsSequence);
            }
            catch (WrongMessageStructure)
            {
                SwiftLogger.Error(Messages.WrongMessageStructure);
                throw new WrongMessageStructure(Messages.WrongMessageStructure);
            }

            repository.InsertIntoDatabase(blocksAndTags);
        }
    }
}