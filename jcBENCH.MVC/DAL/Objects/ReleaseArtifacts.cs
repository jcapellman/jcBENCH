using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace jcBENCH.MVC.DAL.Objects
{
    public class ReleaseArtifacts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }


        [Required]
        public required string Description { get; set; }

        [Required]
        public required string OperatingSystem { get; set; }

        [Required]
        public required string Architecture { get; set; }

        [Required]
        public required string DownloadURI { get; set; }

        public int ReleaseID { get; set; }

        public Releases Release { get; set; }
    }
}
