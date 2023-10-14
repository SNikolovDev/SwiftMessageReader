namespace SwiftMessageReader.Models
{
    public class Block
    {
        public Block(string blockNumber, string blockName, string blockData)
        {
            this.CreatedOn = DateTime.Now;
            this.BlockNumber = blockNumber;
            this.BlockName = blockName;
            this.BlockData = blockData;
        }

        public DateTime CreatedOn { get; set; }

        public string BlockNumber { get; set; }

        public string BlockName { get; set; }

        public string BlockData { get; set; }
    }
}
