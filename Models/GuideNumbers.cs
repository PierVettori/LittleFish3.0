using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class GuideNumbers
    {
        public int Id { get; set; }

        public int Pax { get; set; }


        public int TourTypeId { get; set; }

        public TourType TourType { get; set; }

        public int GuideId { get; set; }

        public Guide Guide { get; set; }

        public DateTime Day { get; set; }
    }
}