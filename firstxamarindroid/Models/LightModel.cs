using System;
using Android.Graphics;
using Realms;

namespace firstxamarindroid.Models
{
    public class LightModel : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public String Name { get; set; }
        public Boolean Status { get; set; }
        public int Brightness { get; set; }
        public Boolean ColorStatus { get; set; }
        public Color LightColor { get; set; }

        public LightModel()
        {

        }

        public LightModel(string name, bool status, int brightness, bool colorStatus, Color lightColor)
        {
            this.Name        = name;
            this.Status      = status;
            this.Brightness  = brightness;
            this.ColorStatus = colorStatus;
            this.LightColor  = lightColor;
        }
    }
}
