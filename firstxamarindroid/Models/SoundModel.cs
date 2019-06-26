using System;
using Realms;

namespace firstxamarindroid.Models
{
    public class SoundModel : RealmObject
    {
        [PrimaryKey]
        public String Id { get; set; }

        public int Volume { get; set; }

        public SoundModel()
        {
        }

        public SoundModel(String id, int volume)
        {
            this.Id         = id;
            this.Volume     = volume;
        }
    }
}
