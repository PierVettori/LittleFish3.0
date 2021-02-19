using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class Guide
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double SalesPercent { get; set; }


        public List<GuideSales> GuideSales { get; set; }

        public Guide()
        {
            GuideSales = new List<GuideSales>();
        }

    }
}