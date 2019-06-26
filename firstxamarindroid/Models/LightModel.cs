using System;
using Android.Graphics;
using Realms;

namespace firstxamarindroid.Models
{
    public class LightModel : RealmObject
    {
        [PrimaryKey]
        public String Id { get; set; }

        public String Name { get; set; }
        public Boolean Status { get; set; }
        public int Brightness { get; set; }
        public Boolean ColorStatus { get; set; }

        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }
        public int ColorA { get; set; }

        public LightModel()
        {

        }

        public LightModel(String id, string name, bool status, int brightness, bool colorStatus, Color lightColor)
        {
            this.Id          = id;
            this.Name        = name;
            this.Status      = status;
            this.Brightness  = brightness;
            this.ColorStatus = colorStatus;

            this.ColorA = lightColor.A;
            this.ColorR = lightColor.R;
            this.ColorG = lightColor.G;
            this.ColorB = lightColor.B;
        }
    }
}
