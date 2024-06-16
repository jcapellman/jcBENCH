namespace jcBENCH.MVC.Models
{
    public class ReleaseResponseItem
    {
        public required string Version { get; set; }

        public required string Description { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public required List<DownloadResponseItem> Downloads { get; set; }
    }
}