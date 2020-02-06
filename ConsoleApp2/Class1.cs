﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    public class Movie
    {
        [XmlElement("MovieName")]
        public string Title
        { get; set; }

        [XmlElement("MovieRating")]
        public float Rating
        { get; set; }

        [XmlElement("MovieReleaseDate")]
        public DateTime ReleaseDate
        { get; set; }
    }
}
