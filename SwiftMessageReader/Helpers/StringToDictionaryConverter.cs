namespace SwiftMessageReader.Helpers
{
    public class StringToDictionaryConverter
    {
        public static Dictionary<int, string> ConvertToDictionary(string text)
        {
            var data = new Dictionary<int, string>();

            var messageArray = text.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            var sendersBankIdentifierCode = messageArray[0].Split(':')[1];
            data.Add(1, sendersBankIdentifierCode);

            var messageRefferenceNumber = messageArray[1].Split(":")[1];
            data.Add(2, messageRefferenceNumber);

            var tag4 = messageArray[2];
            var splittedTag4 = Tag4Splitter(tag4);

            var transactionReferenceNumber = string.Empty;
            var referenceAssinedByTheSender = string.Empty;
            var messageBody = string.Empty;

            for (int i = 0; i < splittedTag4.Length; i++)
            {
                if (splittedTag4[i] == "20")
                {
                    transactionReferenceNumber = splittedTag4[i + 1].TrimEnd();
                }
                if (splittedTag4[i] == "21")
                {
                    referenceAssinedByTheSender = splittedTag4[i + 1].TrimEnd();
                }
                if (splittedTag4[i] == "79")
                {
                    for (int j = i + 1; j < splittedTag4.Length; j++)
                    {
                        messageBody += splittedTag4[j];
                    }

                    messageBody.TrimEnd();
                }
            }

            data.Add(3, transactionReferenceNumber);
            data.Add(4, referenceAssinedByTheSender);
            data.Add(5, messageBody);

            var messageAuthenticationCode = messageArray[4];
            data.Add(6, messageAuthenticationCode);

            var CheckValue = messageArray[5];
            data.Add(7, CheckValue);

            ;
            return data;
        }

        private static string[] Tag4Splitter(string text)
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
    }
}

