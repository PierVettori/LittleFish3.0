using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LittleFish3._0.Models
{
    public class GuideSales
    {
        
        public int Id { get; set; }

        public int sales { get; set; }

        [ForeignKey("Guide")]
        public int GuideId { get; set; }

        
        public Guide Guide { get; set; }

        public DateTime Day { get; set; }
    }
}