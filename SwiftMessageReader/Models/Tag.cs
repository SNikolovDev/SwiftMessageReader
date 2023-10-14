namespace SwiftMessageReader.Models
{
    public class Tag
    {
        public Tag(string tagNumber, string tagName, string TagData)
        {
            this.CreatedOn = DateTime.Now;
            this.TagNumber = tagNumber;
            this.TagName = tagName;
            this.TagData = TagData;
        }

        public DateTime CreatedOn { get; set; }

        public string TagNumber { get; set; }

        public string TagName { get; set; }

        public string TagData { get; set; }
    }
}
