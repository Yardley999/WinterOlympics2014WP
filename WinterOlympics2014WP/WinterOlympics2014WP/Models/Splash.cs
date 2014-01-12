using System;
using System.Runtime.Serialization;

namespace WinterOlympics2014WP.Models
{
    [DataContract]
    public class Splash
    {
        [DataMember(Name = "img")]
        public string Image { get; set; }
    }
}
