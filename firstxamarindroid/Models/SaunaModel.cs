using System;
using System.Collections.Generic;
using System.Linq;
using Realms;

namespace firstxamarindroid.Models
{
    public class SaunaModel : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public String Name { get; set; }

        // Sauna settings properties
        public IList<LightModel> Lights { get; }

        public VentilationModel Ventilation { get; set; }
        public HeaterModel Heater { get; set; }
        public SoundModel Sound { get; set; }


        // Function to return status, if one of the lights are on or not.
        public Boolean LightsStatus
        {
            get { return Lights.First<LightModel>(l => l.Status == true) != null; }
        }

        public SaunaModel()
        {

        }

        public SaunaModel(string name)
        {
            this.Name = name;
        }

        public SaunaModel(string name, IList<LightModel> lights, VentilationModel ventilation, HeaterModel heater, SoundModel sound)
        {
            this.Name           = name;
            this.Lights         = lights;
            this.Ventilation    = ventilation;
            this.Heater         = heater;
            this.Sound          = sound;
        }
    }
}
