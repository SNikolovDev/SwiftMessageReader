using System.Text.RegularExpressions;

using SwiftMessageReader.Models;

namespace SwiftMessageReader.Helpers
{
    public class Parser
    {
        private const string TransactionReferenceNumberTag = "20";
        private const string ReferenceAssignedByTheSenderTag = "21";
        private const string MessageBodyTag = "79";

        private const string TransactionReferenceNumberName = "Transacton reference number";
        private const string ReferenceAssignedByTheSenderName = "Reference assigned by the sender";
        private const string SendersBankIdentifierCodeName = "Senders bank identifier code";
        private const string MessageBodyName = "Message refference number";
        private const string TextHeaderBlockName = "Text header block";
        private const string MessageAuthenticationCodeName = "Message authentication code";
        private const string CheckValueName = "Check value";
        private const string MessageAuthenticationCodeАbbreviation = "MAC";
        private const string CheckValueAbbreviation = "CHK";

        public const string BasicHeaderBlockIdentifier = "1";
        public const string ApplicationHeaderBlockIdentifier = "2";
        public const string TextHeaderBlockIdentifier = "4";
        public const string TrailerHeaderBlockIdentifier = "5";

        public static TransferData SplitByBlocksAndTags(string messageText)
        {
            var transferData = new TransferData();

            var tagsList = new List<Tag>();
            var blocksList = new List<Block>();
            var splittedTextHeaderBlock = new List<string>();

            var messageArray = messageText.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in messageArray)
            {
                Block block = null;

                if (item.StartsWith(BasicHeaderBlockIdentifier))
                {
                    block = new Block(item.Take(1).ToString(), SendersBankIdentifierCodeName, item.Substring(2));
                }
                else if (item.StartsWith(ApplicationHeaderBlockIdentifier))
                {
                    block = new Block(item.Take(1).ToString(), MessageBodyName, item.Substring(2));
                }
                else if (item.StartsWith(TextHeaderBlockIdentifier))
                {
                    TagVerifier(item);
                    splittedTextHeaderBlock = TextHeaderBlockSplitter(item);
                    block = new Block(item.Take(1).ToString(), TextHeaderBlockName, item.Substring(2).TrimEnd());
                }
                else if (item.StartsWith(MessageAuthenticationCodeАbbreviation))
                {
                    block = new Block(TrailerHeaderBlockIdentifier, MessageAuthenticationCodeName, item);
                }
                else if (item.StartsWith(CheckValueAbbreviation))
                {
                    block = new Block(TrailerHeaderBlockIdentifier, CheckValueName, item);
                }

                if (block != null)
                {
                    blocksList.Add(block);
                }
            }
            var messageBody = string.Empty;

            for (int i = 1; i < splittedTextHeaderBlock.Count; i += 2)
            {
                if (splittedTextHeaderBlock[i] == TransactionReferenceNumberTag)
                {
                    tagsList.Add(new Tag(TransactionReferenceNumberTag, TransactionReferenceNumberName, splittedTextHeaderBlock[i + 1].TrimEnd()));
                }
                else if (splittedTextHeaderBlock[i] == ReferenceAssignedByTheSenderTag)
                {
                    tagsList.Add(new Tag(ReferenceAssignedByTheSenderTag, ReferenceAssignedByTheSenderName, splittedTextHeaderBlock[i + 1].TrimEnd()));

                }
                else if (splittedTextHeaderBlock[i] == MessageBodyTag)
                {
                    for (int j = i + 1; j < splittedTextHeaderBlock.Count; j++)
                    {
                        messageBody += splittedTextHeaderBlock[j];
                    }

                    messageBody.TrimEnd();

                    tagsList.Add(new Tag(MessageBodyTag, MessageBodyName, messageBody));
                }
            }

            transferData.BlocksList = blocksList;
            transferData.TagsList = tagsList;

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

        private static void TagVerifier(string text)
        {
            var pattern = "^\\d\\:\\n?\\:\\d{2}\\:[0-9A-Z-]*\\s?\\n?:\\d{2}\\:[0-9A-Z-]*\\s?\\n?:\\d{2}\\:[\\x20-\\x2F\\x3A-\\x40A-Z0-9\\n]+[-]?$";
            text = text.Replace("\r", "");
            var isMatched = Regex.IsMatch(text, pattern);

            if (!isMatched)
            {
                throw new Exception();
            }
        }
    }
}

