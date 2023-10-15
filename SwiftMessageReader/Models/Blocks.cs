namespace SwiftMessageReader.Models
{
    public class Blocks
    {
        public Blocks()
        {
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public string SendersBankIdentifierCode { get; set; }

        public string MessageReferenceNumber { get; set; }

        public string MessageAuthenticationCode { get; set; }

        public string CheckValue { get; set; }
    }
}