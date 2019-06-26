using System;
using Realms;

namespace firstxamarindroid.Models
{
    public class VentilationModel : RealmObject
    {
        [PrimaryKey]
        public String Id { get; set; }

       
        public Boolean Status { get; set; }
        public long OpenedAt { get; set; }

        public Boolean Fan1 { get; set; }
        public Boolean Fan2 { get; set; }

        public int FanRunningTime1 { get; set; }
        public long Fan1ClosingAt { get; set; }
        public int FanRunningTime2 { get; set; }
        public long Fan2ClosingAt { get; set; }

        public VentilationModel()
        {
        }

        public VentilationModel(String id, bool status, long openedAt,
            bool fanStatus1, bool fanStatus2, int fanRunningTime1, long fan1ClosingAt,
            int fanRunningTime2, long fan2ClosingAt)
        {
            this.Id             = id;
            this.Status         = status;
            this.OpenedAt       = openedAt;
            this.Fan1           = fanStatus1;
            this.Fan2           = fanStatus2;
            this.FanRunningTime1= fanRunningTime1;
            this.Fan1ClosingAt  = fan1ClosingAt;
            this.FanRunningTime2= fanRunningTime2;
            this.Fan2ClosingAt  = fan2ClosingAt;
        }
    }
}
