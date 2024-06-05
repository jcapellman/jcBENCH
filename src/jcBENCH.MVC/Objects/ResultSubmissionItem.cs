using System.Runtime.Serialization;

namespace jcBENCH.MVC.Objects
{
    [DataContract]
    public class ResultSubmissionItem
    {
        [DataMember]
        public required string os_name { get; set; }

        [DataMember] 
        public required string cpu_name { get; set; }

        [DataMember]
        public required string cpu_architecture { get; set; }

        [DataMember]
        public int cpu_cores { get; set; }

        [DataMember]
        public int score { get; set; }

        [DataMember]
        public required string benchmark_name { get; set; }
    }
}