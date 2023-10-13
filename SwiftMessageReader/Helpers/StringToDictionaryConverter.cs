using System.Text.RegularExpressions;

namespace SwiftMessageReader.Helpers
{
    public class StringToDictionaryConverter
    {
        public static Dictionary<int, string> ConvertToDictionary(string text)
        {
            var data = new Dictionary<int, string>();

            var messageArray = text.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            var sendersBankIdentifierCode = messageArray[0].Split(':')[1];
            data.Add(Keys.SendersBankIdentifierKey, sendersBankIdentifierCode);

            var messageRefferenceNumber = messageArray[1].Split(":")[1];
            data.Add(Keys.MessageReferenceNumberKey, messageRefferenceNumber);

            var tag4 = messageArray[2].TrimEnd();
            TagVerifier(tag4);
            ;
            var splittedTag4 = TextHeaderBlockSplitter(tag4);

            var transactionReferenceNumber = string.Empty;
            var referenceAssinedByTheSender = string.Empty;
            var messageBody = string.Empty;



            for (int i = 0; i < splittedTag4.Length; i++)
            {
                if (splittedTag4[i] == Keys.TransactionReferenceNumberKey.ToString())
                {
                    transactionReferenceNumber = splittedTag4[i + 1].TrimEnd();
                }
                if (splittedTag4[i] == Keys.ReferenceAssinedByTheSenderKey.ToString())
                {
                    referenceAssinedByTheSender = splittedTag4[i + 1].TrimEnd();
                }
                if (splittedTag4[i] == Keys.MessageBodyKey.ToString())
                {
                    for (int j = i + 1; j < splittedTag4.Length; j++)
                    {
                        messageBody += splittedTag4[j];
                    }

                    messageBody.TrimEnd();
                }
            }

            data.Add(Keys.TransactionReferenceNumberKey, transactionReferenceNumber);
            data.Add(Keys.ReferenceAssinedByTheSenderKey, referenceAssinedByTheSender);
            data.Add(Keys.MessageBodyKey, messageBody);

            var messageAuthenticationCode = messageArray[4];
            data.Add(Keys.MessageAuthenticationCodeKey, messageAuthenticationCode);

            var CheckValue = messageArray[5];
            data.Add(Keys.CheckValueKey, CheckValue);

            return data;
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

