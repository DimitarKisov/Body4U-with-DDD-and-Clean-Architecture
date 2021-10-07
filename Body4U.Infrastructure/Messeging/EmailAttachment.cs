namespace Body4U.Infrastructure.Messeging
{
    public class EmailAttachment
    {
        public EmailAttachment(byte[] content, string fileName, string mimeType)
        {
            this.Content = content;
            this.FileName = fileName;
            this.MimeType = mimeType;
        }

        public byte[] Content { get; }

        public string FileName { get; }

        public string MimeType { get; }
    }
}
