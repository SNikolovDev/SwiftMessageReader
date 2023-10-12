namespace SwiftMessageReader.Helpers
{
    public static class Keys
    {
       /// <summary>
       /// Custom keys, to give corresponding value of the data in the dictionary which contains the information from the text file.
       /// </summary>
        public const int SendersBankIdentifierKey = 1;
        public const int MessageReferenceNumberKey = 2;
        public const int TransactionReferenceNumberKey = 3;
        public const int ReferenceAssinedByTheSenderKey = 4;
        public const int MessageBodyKey = 5;
        public const int MessageAuthenticationCodeKey = 6;
        public const int CheckValueKey = 7;
    }
}
