namespace SwiftMessageReader.Models
{
    public class Blocks
    {
        public DateTime CreatedOn { get; set; }

        // {1:F01PRCBBGSFAXXX1111111111}
        // F01 - type of senders reference;
        // (PRCB - bank code; BG - country code; SF - location code; AXXX - branch code) - BIC;
        // 1111111111 - unique identifier;
        public int BasicHeaderBlock { get; set; } // beginning of the message block
        public string SendersBankIdentifierCode { get; set; }

        // {2:O7991111111111ABGRSWACAXXX11111111111111111111N}
        // O799 - message type; 1111111111 - sender's unique reference number;
        // ABGRSWACAXXX - receiver's BIC;
        // 11111111111111111111 - additional info/ transaction reference number;
        // N - priority (Normal);
        public int ApplicationHeaderBlock { get; set; }
        public string MessageReferenceNumber { get; set; }

        //{5:
        public string TrailerHeaderBlock { get; set; }

        // {MAC:00000000}
        public string MessageAuthenticationCode { get; set; }

        // {CHK:111111111111}
        public string CheckValue { get; set; }
        // } closing tag 5
    }
}
