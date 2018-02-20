using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jcBENCH.web.DAL.Objects
{
    public class Results
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public string BenchmarkID { get; set; }

        [Required]
        [ForeignKey("BenchmarkID")]
        public Benchmarks Benchmark { get; set; }

        [Required]
        public string PlatformID { get; set; }
        
        [ForeignKey("PlatformID")]
        public Platforms Platform { get; set; }

        [Required]
        public string CPUManufacturer { get; set; }

        [Required]
        public string CPUName { get; set; }

        [Required]
        public string OperatingSystem { get; set; }

        [Required]
        public string CPUFrequency { get; set; }

        [Required]
        public string CPUArchitecture { get; set; }

        [Required]
        public int BenchmarkResult { get; set; }
    }
}