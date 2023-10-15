using SwiftMessageReader.Helpers;

namespace SwiftMessageReader.Models
{
    public class Tags
    {
        public Tags()
        {
            this.CreatedOn = DateTime.Now;

            this.TransactionReferenceNumberTag = TagsConstants.TransactionReferenceNumberTag;
            this.ReferenceAssinedByTheSenderTag = TagsConstants.ReferenceAssignedByTheSenderTag;
            this.MessageBodyTag = TagsConstants.MessageBodyTag;
        }

        public DateTime CreatedOn { get; set; }

        public string TransactionReferenceNumberTag { get; set; }
        public string TransactionReferenceNumber { get; set; }

        public string ReferenceAssinedByTheSenderTag { get; set; }
        public string ReferenceAssinedByTheSender { get; set; }

        public string MessageBodyTag { get; set; }
        public string MessageBody { get; set; }
    }
}