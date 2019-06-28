using System;
namespace firstxamarindroid.Models
{
    public class HeaterSensorModel
    {
        private int Id { get; set; }
        private Double Temperature { get; set; }

        public HeaterSensorModel()
        {

        }

        public HeaterSensorModel(int id, double temperature)
        {
            this.Id             = id;
            this.Temperature    = temperature;
        }
    }
}
