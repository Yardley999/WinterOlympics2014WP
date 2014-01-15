using System;
using System.Runtime.Serialization;

namespace WinterOlympics2014WP.Models
{
    [DataContract]
    public class EPG
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "start")]
        public DateTime Start { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "cate")]
        public string Category { get; set; }

        [DataMember(Name = "match")]
        public string Match { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "seepoint")]
        public string SeePoint { get; set; }

        [DataMember(Name = "gold")]
        public int Gold { get; set; }
    }
}
