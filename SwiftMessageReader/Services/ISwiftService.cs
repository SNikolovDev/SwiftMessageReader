using SwiftMessageReader.Models;

namespace SwiftMessageReader.Services
{
    public interface ISwiftService
    {
        void ManageFile(IFormFile file);
    }
}