using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jcBENCH.MVC.DAL.Objects
{
    public class Results
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;

        [Required]
        public required string BenchmarkName { get; set; }

        [Required]
        public required string CPUName { get; set; }

        [Required]
        public required string OperatingSystem { get; set; }

        [Required]
        public required string CPUArchitecture { get; set; }

        [Required]
        public int BenchmarkResult { get; set; }
    }
}