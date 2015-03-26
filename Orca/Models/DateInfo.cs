using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orca.Models
{
    public class DateInfo
    {
        [DisplayName("Start date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public DateInfo(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
        }
        public DateInfo() { }
    }
}