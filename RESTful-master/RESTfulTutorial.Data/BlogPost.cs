﻿namespace RESTfulTutorial.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract]
    public class BlogPost
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required]
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [Required]
        [Column("Url")]
        [DataMember(Name = "url")]
        public string UriString
        {
            get
            {
                return Url == null ? null : Url.ToString();
            }
            set
            {
                Url = value == null ? null : new Uri(value);
            }
        }

        [NotMapped]
        public Uri Url { get; set; }
    }
}