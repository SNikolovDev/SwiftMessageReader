using SwiftMessageReader.Models;

namespace SwiftMessageReader.Services.Interfaces
{
    public interface ISwiftService
    {
        void ManageFile(IFormFile file);
    }
}