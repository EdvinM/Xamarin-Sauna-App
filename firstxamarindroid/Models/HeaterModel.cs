using System;
using Realms;

namespace firstxamarindroid.Models
{
    public class HeaterModel : RealmObject
    {
        [PrimaryKey]
        public String Id { get; set; }

        public Boolean Status { get; set; }
        public String Power { get; set; }
        public int TemperatureThreshold { get; set; }

        public HeaterModel()
        {

        }

        public HeaterModel(String id, bool status, string power, int temperatureThreshold)
        {
            this.Id                     = id;
            this.Status                 = status;
            this.Power                  = power;
            this.TemperatureThreshold   = temperatureThreshold;
        }
    }
}
