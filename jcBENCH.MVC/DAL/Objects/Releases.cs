using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace jcBENCH.MVC.DAL.Objects
{
    public class Releases
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required]
        public required DateTime ReleaseDate { get; set; }

        [Required]
        public required bool IsPreRelease { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        public ICollection<ReleaseArtifacts> ReleaseArtifacts { get; } = new List<ReleaseArtifacts>();
    }
}