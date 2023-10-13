namespace SwiftMessageReader.Models
{
    public class Tag
    {
        public Tag(string tagNumber, string tagName, string TagData)
        {
            this.TagNumber = tagNumber;
            this.TagName = tagName;
            this.TagData = TagData;
        }

        public string TagNumber { get; set; }

        public string TagName { get; set; }

        public string TagData { get; set; }
    }
}
