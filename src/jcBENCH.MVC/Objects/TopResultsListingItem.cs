namespace jcBENCH.MVC.Objects
{
    public class TopResultsListingItem
    {
        public required string CPUManufacturer { get; set; }

        public required string BenchmarkName { get; set; }

        public required string CPUModelName { get; set; }

        public int BenchmarkResult { get; set; }
    }
}