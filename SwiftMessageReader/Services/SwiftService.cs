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
                throw new Exception(ex.Message);
            }

            var blocksAndTags = Parser.SplitByBlocksAndTags(fileAsString);


            var model = MessageModelMapper(blocksAndTags);
            repository.InsertIntoDatabase(model);
        }

        private TransferData MessageModelMapper(TransferData data)
        {
            var returnModel = new TransferData();
            var blocks = new List<Block>();
            var tags = new List<Tag>();

            foreach (var block in data.BlocksList)
            {
                var newBlock = new Block(block.BlockNumber, block.BlockName, block.BlockData);
                blocks.Add(newBlock);
            }

            foreach (var tag in data.TagsList)
            {
                var currentTag = new Tag(tag.TagNumber, tag.TagName, tag.TagData);
                tags.Add(currentTag);
            }

            returnModel.BlocksList = blocks;
            returnModel.TagsList = tags;

            SwiftLogger.Info(Messages.SuccessfulMapping);

            return returnModel;
        }
    }
}
