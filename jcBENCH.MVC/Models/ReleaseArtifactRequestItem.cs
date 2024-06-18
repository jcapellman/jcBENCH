namespace jcBENCH.MVC.Models
{
    public class ReleaseArtifactRequestItem
    {
        public required string Description { get; set; }

        public required string DownloadURI { get; set; }

        public required string Architecture { get; set; }

        public required string OperatingSystem { get; set; }
    }
}
