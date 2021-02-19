using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class Tour
    {

        public int Id { get; set; }

        //[ForeignKey("Day")]
        public DateTime Day { get; set; }
        
        public int Pax  { get; set; }


        [ForeignKey("TourType")]
        public int TourTypeId { get; set; }

        public TourType TourType { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public People Person { get; set; }


       


    }
}