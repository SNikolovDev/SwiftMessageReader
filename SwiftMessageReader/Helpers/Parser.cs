using System.Text.RegularExpressions;

using SwiftMessageReader.Models;

namespace SwiftMessageReader.Helpers
{
    public class Parser
    {    
        private const string TransactionReferenceNumberTag = "20";
        private const string ReferenceAssignedByTheSenderTag = "21";
        private const string MessageBodyTag = "79";

        public static DataClass SplitDataTypes(string text)
        {
            var headerBlocks = new Dictionary<string, string>();
            var tagsList = new List<Tag>();
            var dataClass = new DataClass();

            var messageArray = text.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            var sendersBankIdentifierCode = string.Empty;
            var messageRefferenceNumber = string.Empty;
            var textHeaderBlock = string.Empty;
            var messageAuthenticationCode = string.Empty;
            var checkValue = string.Empty;

            foreach (var item in messageArray)
            {
                if (item.StartsWith('1'))
                {
                    sendersBankIdentifierCode = item.Substring(2);
                }
                else if (item.StartsWith('2'))
                {
                    messageRefferenceNumber = item.Substring(2);
                }
                else if (item.StartsWith('4'))
                {
                    textHeaderBlock = item.TrimEnd();
                }
                else if (item.StartsWith("MAC"))
                {
                    messageAuthenticationCode = item;
                }
                else if (item.StartsWith("CHK"))
                {
                    checkValue = item;
                }
            }

            TagVerifier(textHeaderBlock);
            ;
            var splittedTextHeaderBlock = TextHeaderBlockSplitter(textHeaderBlock);

            var transactionReferenceNumber = string.Empty;
            var referenceAssinedByTheSender = string.Empty;
            var messageBody = string.Empty;

            for (int i = 1; i < splittedTextHeaderBlock.Length; i += 2)
            {
                if (splittedTextHeaderBlock[i] == TransactionReferenceNumberTag)
                {
                    tagsList.Add(new Tag(TransactionReferenceNumberTag, nameof(transactionReferenceNumber), splittedTextHeaderBlock[i + 1].TrimEnd()));
                }
                else if (splittedTextHeaderBlock[i] == ReferenceAssignedByTheSenderTag)
                {
                    tagsList.Add(new Tag(ReferenceAssignedByTheSenderTag, nameof(referenceAssinedByTheSender), splittedTextHeaderBlock[i + 1].TrimEnd()));

                }
                else if (splittedTextHeaderBlock[i] == MessageBodyTag)
                {
                    for (int j = i + 1; j < splittedTextHeaderBlock.Length; j++)
                    {
                        messageBody += splittedTextHeaderBlock[j];
                    }

                    messageBody.TrimEnd();

                    tagsList.Add(new Tag(MessageBodyTag, nameof(messageBody), messageBody));
                }
            }
            ;
            headerBlocks.Add(HeaderBlocks.BasicHeaderBlockIdentifier, sendersBankIdentifierCode);
            headerBlocks.Add(HeaderBlocks.ApplicationHeaderBlockIdentifier, messageRefferenceNumber);
            headerBlocks.Add(HeaderBlocks.TrailerHeaderBlockIdentifier1, messageAuthenticationCode);
            headerBlocks.Add(HeaderBlocks.TrailerHeaderBlockIdentifier2, checkValue);

            dataClass.HeaderBlocks = headerBlocks;
            dataClass.TagsList = tagsList;

            return dataClass;
        }

        private static string[] TextHeaderBlockSplitter(string text)
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

            var resultArray = resultList.ToArray();
            return resultArray;
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

