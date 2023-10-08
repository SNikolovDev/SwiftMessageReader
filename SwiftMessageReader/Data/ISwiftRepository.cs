using SwiftMessageReader.Models;

namespace SwiftMessageReader.Data
{
    public interface ISwiftRepository
    {
        void InsertIntoDatabase(MessageModel model);
    }
}