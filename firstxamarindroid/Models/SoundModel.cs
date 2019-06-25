using System;
using Realms;

namespace firstxamarindroid.Models
{
    public class SoundModel : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int Volume { get; set; }

        public SoundModel()
        {
        }

        public SoundModel(int id, int volume)
        {
            this.Id         = id;
            this.Volume     = volume;
        }
    }
}
