using System;
using Realms;

namespace firstxamarindroid.Models
{
    public class VentilationModel : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

       
        public Boolean Status { get; set; }

        public Boolean Fan1 { get; set; }
        public Boolean Fan2 { get; set; }

        public int FanRunningTime1 { get; set; }
        public int FanRunningTime2 { get; set; }

        public VentilationModel()
        {
        }

        public VentilationModel(bool status, bool fanStatus1, bool fanStatus2, int fanRunningTime1, int fanRunningTime2)
        {
            this.Status         = status;
            this.Fan1           = fanStatus1;
            this.Fan2           = fanStatus2;
            this.FanRunningTime1= fanRunningTime1;
            this.FanRunningTime2= fanRunningTime2;
        }
    }
}
