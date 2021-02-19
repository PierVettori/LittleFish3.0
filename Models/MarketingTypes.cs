using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class MarketingTypes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("MarketingOrder")]
        public int MarketingOrderId { get; set; }

        public MarketingOrder MarketingOrder { get; set; }

        public int Order { get; set; }
        public int LeadsTo { get; set; }
    }
}