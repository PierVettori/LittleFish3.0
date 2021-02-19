using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class MarketingOrder
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<MarketingTypes> Marketingtypes { get; set; }


    }
}