﻿using SwiftMessageReader.Data.Interfaces;
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
        }

        private MessageModel MessageModelMapper(DataClass data)
        {
            var model = new MessageModel();

            model.CreatedOn = DateTime.Now;

            model.SendersBankIdentifierCode = data.HeaderBlocks["1"];
            model.MessageReferenceNumber = data.HeaderBlocks["2"];

            model.TransactionReferenceNumber = data.TagsList[0].TagData;
            model.ReferenceAssinedByTheSender = data.TagsList[1].TagData;
            model.MessageBody = data.TagsList[2].TagData;

            model.MessageAuthenticationCode = data.HeaderBlocks["5.1"];
            model.CheckValue = data.HeaderBlocks["5.2"];

            SwiftLogger.Info(Messages.SuccessfulMapping);

            return model;
        }
    }
}
