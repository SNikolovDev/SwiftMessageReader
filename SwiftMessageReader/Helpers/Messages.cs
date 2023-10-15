namespace SwiftMessageReader.Helpers
{
    public static class Messages
    {       
        public const string SuccessfulDataInsert = "Data inserted successfuly to the table.";
        public const string SuccessfulUpload = "File uploaded successful.";
        public const string SuccessfulMapping = "Data from file successfuly mapped to message model.";
        public const string SuccessfulDatabaseCreation = "Database created successfuly.";
        public const string FileReadSuccessfuly = "Finished file reading.";


        public const string UploadFailedError = "An error occured when trying to upload the file.";
        public const string Data = "An error occured when trying to upload the file.";
        public const string InvalidFileError = "Invalid file.";
        public const string DatabaseCreateError = "An error occurred when trying to create the database: ";
        public const string WrongBracketsSequence = "Message structure is wrong. Check the curly brackets and upload the file again.";
        public const string WrongMessageStructure = "Message structure is wrong. Check it and upload the file again.";
        public const string InvalidFileExceptionMessage = "The file is null or empty.";
    }
}
