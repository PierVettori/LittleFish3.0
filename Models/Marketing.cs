using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class Marketing
    {
        public int Id { get; set; }

       

        public int MarketTypeId { get; set; }

        public MarketingTypes MarketingType { get; set; }

        public int Pax { get; set; }

        public DateTime Day { get; set; }
    }
}