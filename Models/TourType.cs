using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class TourType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }

        [NotMapped]
        public int Pax { get; set; }

        public List<Tour> Tours { get; set; }

        public List<GuideNumbers> GuideNumbers { get; set; }

        public List<People> People { get; set; }

        public TourType()
        {
            Tours = new List<Tour>();
            GuideNumbers = new List<GuideNumbers>();
            People = new List<People>();

            
        }

       

       

    }
}