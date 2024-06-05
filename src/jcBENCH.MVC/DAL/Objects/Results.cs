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
        public string BenchmarkName { get; set; }

        [Required]
        public string CPUName { get; set; }

        [Required]
        public string OperatingSystem { get; set; }

        [Required]
        public string CPUArchitecture { get; set; }

        [Required]
        public int BenchmarkResult { get; set; }
    }
}