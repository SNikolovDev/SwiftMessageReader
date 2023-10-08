namespace SwiftMessageReader.Models
{
    public class MessageModel
    {
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

        // {4:
        public int TextHeaderBlock { get; set; }

        // :20:67-C111111-KNTRL 
        public int FieldTag1 { get; set; } // "20" - transaction reference number for the message
        public string TransactionReferenceNumber { get; set; }

        // :21:30-111-1111111
        public int FieldTag2 { get; set; } // "21" - reference assined by the sender
        public string RelatedReferance { get; set; }

        // :79:NA VNIMANIETO NA: OTDEL BANKOVI GARANTSII
        public int FieldTag4 { get; set; } // "79" - free format message
        public string MessageBody { get; set; }

        // -} closing tag 4

        //{5:
        public int TrailerHeaderBlock { get; set; }

        // {MAC:00000000}
        public int MessageAuthenticationCode { get; set; }

        // {CHK:111111111111}
        public int CheckValue { get; set; }
        // } closing tag 5
    }
}
