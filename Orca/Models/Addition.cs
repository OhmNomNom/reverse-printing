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
        [Required, MaxLength(8), DisplayName("AUBnet Account")]
        public string AUBnet
        {
            get
            {
                return _aubnet;
            }
            set 
            {
                this._aubnet = value;
                this._major = AUB.getMajor(value);
                this._factor = AUB.getFactorForMajor(this.Major);
            } 
        }

        private string _major = "", _aubnet = "";
        private double _factor = 1;

        [Required, Range(0.01,100), DisplayName("Kilograms Donated")]
        public double Kilos{get; set;}

        public DateTime Timestamp { get; set; }

        public bool Processed { get; set; }

        public double Quota { get { return Kilos * _factor; } }

        public string Major { get { return _major; } }
        
    }
}