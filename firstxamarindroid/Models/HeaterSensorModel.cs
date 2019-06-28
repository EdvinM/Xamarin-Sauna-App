using System;
namespace firstxamarindroid.Models
{
    public class HeaterSensorModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Double Temperature { get; set; }

        public HeaterSensorModel()
        {

        }

        public HeaterSensorModel(int id, String name, double temperature)
        {
            this.Id             = id;
            this.Name           = name;
            this.Temperature    = temperature;
        }
    }
}
