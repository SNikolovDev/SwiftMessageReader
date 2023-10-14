namespace SwiftMessageReader.Models
{
    public class TransferData
    {
        public Dictionary<string, string> HeaderBlocks { get; set; }

        public List<Tag> TagsList { get; set; }
    }
}
