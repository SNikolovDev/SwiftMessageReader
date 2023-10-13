using SwiftMessageReader.Helpers;

public class FileToStringConverter
{
    public static string ConvertIFormFileToString(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException();
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(memoryStream, System.Text.Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            SwiftLogger.Error(Messages.InvalidFileError);
            throw new ArgumentException(ex.Message);
        }
    }
}