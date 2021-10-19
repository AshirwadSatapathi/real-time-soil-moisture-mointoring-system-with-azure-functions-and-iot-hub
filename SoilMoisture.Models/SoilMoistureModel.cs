using System;
using System.Collections.Generic;
using System.Text;

namespace SoilMoisture.Models
{
    public class SoilMoistureModel
    {
        public string deviceId { get; set; }
        public DateTime recordedAt { get; set; }
        public double moistureLevel { get; set; }
        
    }
}
