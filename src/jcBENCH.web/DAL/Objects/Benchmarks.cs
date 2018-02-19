using System.ComponentModel.DataAnnotations;

namespace jcBENCH.web.DAL.Objects
{
    public class Benchmarks
    {
        [Key]
        public string Name { get; set; }
    }
}