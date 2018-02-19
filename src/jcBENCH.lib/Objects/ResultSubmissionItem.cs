using System.Runtime.Serialization;

namespace jcBENCH.lib.Objects
{
    [DataContract]
    public class ResultSubmissionItem
    {
        [DataMember]
        public int BenchmarkResult { get; set; }

        [DataMember]
        public string BenchmarkID { get; set; }

        [DataMember]
        public string PlatformID { get; set; }

        [DataMember]
        public string CPUName { get; set; }

        [DataMember]
        public string CPUManufacturer { get; set; }

        [DataMember]
        public string CPUArchitecture { get; set; }

        [DataMember]
        public string CPUFrequency { get; set; }

        [DataMember]
        public string OperatingSystem { get; set; }
    }
}