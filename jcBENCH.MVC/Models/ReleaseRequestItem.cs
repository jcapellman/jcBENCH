namespace jcBENCH.MVC.Models
{
    public class ReleaseRequestItem
    {
        public required string Name { get; set; }

        public required DateTime ReleaseDate { get; set; }

        public required string Description { get; set; }

        public required bool IsPreRelease { get; set; }

        public List<ReleaseArtifactRequestItem> Artifacts { get; set; }
    }
}