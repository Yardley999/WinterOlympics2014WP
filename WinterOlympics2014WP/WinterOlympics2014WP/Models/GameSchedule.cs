using System;
using System.Runtime.Serialization;
using WinterOlympics2014WP.Infrastructures;

namespace WinterOlympics2014WP.Models
{
    [DataContract]
    public class GameSchedule : BindableBase
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "cate")]
        public string Category { get; set; }

        [DataMember(Name = "start")]
        public DateTime StartTime { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "match")]
        public string Match { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "end")]
        public DateTime EndTime { get; set; }

        private bool subscribed = false;
        public bool Subscribed
        {
            get { return subscribed; }
            set { SetProperty(ref this.subscribed, value); }
        }
    }
}
