using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jcBENCH.MVC.DAL.Objects
{
    public class Results
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required]
        public required string CPUName { get; set; }

        [Required]
        public required string OperatingSystem { get; set; }

        [Required]
        public required string CPUArchitecture { get; set; }

        [Required]
        public int CPUCoreCount { get; set; }

        [Required]
        public required string BenchmarkName { get; set; }

        [Required]
        public int BenchmarkResult { get; set; }

        [Required]
        public int BenchmarkAPIVersion { get; set; }

        [Required]
        public required string BenchmarkThreadingModel { get; set; }
    }
}