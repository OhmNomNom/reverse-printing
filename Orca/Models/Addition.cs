using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Orca.Models
{
    public class Addition
    {
        private double _factor = 1;

        [Required, MaxLength(8), DisplayName("AUBnet Account")]
        public string AUBnet{get; set;}

        [Required, Range(0.01,100), DisplayName("Kilograms Donated")]
        public double Kilos{get; set;}

        public DateTime Timestamp { get; set; }

        public double Quota { get { return Kilos * _factor; } }

        public void setFactor(double factor)
        {
            _factor = factor;
        }
        
    }
}