
using System.Runtime.Serialization;
namespace WinterOlympics2014WP.Models
{
    [DataContract]
    public class News
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        //[DataMember]
        //public bool hasfocus { get; set; }

        //[DataMember]
        //public bool view { get; set; }
    }

    [DataContract]
    public class NewsList
    {
        [DataMember]
        public News[] data { get; set; }
    }
}
