﻿using System;
using System.Runtime.Serialization;

namespace WinterOlympics2014WP.Models
{
    [DataContract]
    public class HTML
    {
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
