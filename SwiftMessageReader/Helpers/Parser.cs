using SwiftMessageReader.Models;

namespace SwiftMessageReader.Helpers
{
    public static class Parser
    {
        private const string MessageAuthenticationCodeАbbreviation = "MAC";
        private const string CheckValueAbbreviation = "CHK";

        public const string BasicHeaderBlockIdentifier = "1";
        public const string ApplicationHeaderBlockIdentifier = "2";
        public const string TextHeaderBlockIdentifier = "4";
        public const string TrailerHeaderBlockIdentifier = "5";

        public static TransferData SplitByBlocksAndTags(string messageText)
        {
            var transferData = new TransferData();
            var blocks = new Blocks();
            var tags = new Tags();
            var splittedTextHeaderBlock = new List<string>();

            var messageArray = messageText.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in messageArray)
            {
                if (item.StartsWith(BasicHeaderBlockIdentifier))
                {
                    blocks.SendersBankIdentifierCode = item[2..];
                }
                if (item.StartsWith(ApplicationHeaderBlockIdentifier))
                {
                    blocks.MessageReferenceNumber = item[2..];
                }
                if (item.StartsWith(TextHeaderBlockIdentifier))
                {
                    MessageStructureVerifiers.TagVerifier(item);
                    splittedTextHeaderBlock = TextHeaderBlockSplitter(item);
                }
                if (item.StartsWith(MessageAuthenticationCodeАbbreviation))
                {
                    blocks.MessageAuthenticationCode = item;
                }
                if (item.StartsWith(CheckValueAbbreviation))
                {
                    blocks.CheckValue = item;
                }
            }

            var messageBody = string.Empty;

            for (int i = 1; i < splittedTextHeaderBlock.Count; i += 2)
            {
                if (splittedTextHeaderBlock[i] == TagConstants.TransactionReferenceNumberTag)
                {
                    tags.TransactionReferenceNumber = splittedTextHeaderBlock[i + 1].TrimEnd();
                }
                if (splittedTextHeaderBlock[i] == TagConstants.ReferenceAssignedByTheSenderTag)
                {
                    tags.ReferenceAssinedByTheSender = splittedTextHeaderBlock[i + 1].TrimEnd();
                }
                if (splittedTextHeaderBlock[i] == TagConstants.MessageBodyTag)
                {
                    for (int j = i + 1; j < splittedTextHeaderBlock.Count; j++)
                    {
                        messageBody += splittedTextHeaderBlock[j];
                    }

                    tags.MessageBody = messageBody.TrimEnd();
                }
            }

            transferData.Blocks = blocks;
            transferData.Tags = tags;
            return transferData;
        }

        private static List<string> TextHeaderBlockSplitter(string text)
        {
            var parts = text.Split(':').ToList();

            List<string> resultList = new List<string>();

            bool found79 = false;
            string after79 = string.Empty;

            foreach (string part in parts)
            {
                if (part == "\r\n")
                {
                    continue;
                }
                if (found79)
                {
                    after79 += ":" + part;
                }
                else
                {
                    if (part == "79")
                    {
                        found79 = true;
                    }

                    resultList.Add(part);
                }
            }

            resultList.Add(after79.TrimStart(':'));
            return resultList;
        }
    }
}