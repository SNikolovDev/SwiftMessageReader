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

            var data = Parser.SplitDataTypes(text);
            var model = MessageModelMapper(data);
            repository.InsertIntoDatabase(model);
        }

        private TransferDataToRepository MessageModelMapper(TransferData data)
        {
            var insertModel = new TransferDataToRepository();
            var model = new Blocks();
            var tags = new List<Tag>();

            model.CreatedOn = DateTime.Now;

            model.SendersBankIdentifierCode = data.HeaderBlocks[HeaderBlocks.BasicHeaderBlockIdentifier];
            model.MessageReferenceNumber = data.HeaderBlocks[HeaderBlocks.ApplicationHeaderBlockIdentifier];

            foreach (var tag in data.TagsList)
            {
                var currentTag = new Tag(tag.TagNumber, tag.TagName, tag.TagData);
                tags.Add(currentTag);
            }


            model.MessageAuthenticationCode = data.HeaderBlocks[HeaderBlocks.TrailerHeaderBlockIdentifier1];
            model.CheckValue = data.HeaderBlocks[HeaderBlocks.TrailerHeaderBlockIdentifier2];

            SwiftLogger.Info(Messages.SuccessfulMapping);

            insertModel.Model = model;
            insertModel.Tags = tags;

            return insertModel;
        }
    }
}
