using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class People
    {
        public int Id { get; set; }

        public string Name { get; set; }

       
        public int TourId { get; set; }

        public TourType TourType { get; set; }

        //public List<Tour> Tours { get; set; }

        //public People()
        //{
        //    Tours = new List<Tour>();
        //}



    }
}