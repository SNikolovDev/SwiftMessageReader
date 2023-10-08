
using SwiftMessageReader.Helpers;

public class FileToStringConverter
{
    public static string ConvertIFormFileToString(IFormFile file)
    {
        try
        {
            // Check if the file is not null
            if (file == null || file.Length == 0)
            {
                SwiftLogger.Error("Error when uploading.");
                throw new ArgumentNullException("File is null");
            }

            using (var memoryStream = new MemoryStream())
            {
                // Copy the content of the IFormFile to a MemoryStream
                file.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Assuming the file is in UTF-8 encoding; adjust as needed
                using (var reader = new StreamReader(memoryStream, System.Text.Encoding.UTF8))
                {
                    return reader.ReadToEnd(); // Convert the bytes to a string
                }
            }
        }
        catch (Exception ex)
        {
            SwiftLogger.Error("Error when uploading.");
            throw new Exception(ex.Message);
        }
    }
}