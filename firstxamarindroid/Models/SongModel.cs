using System;
namespace firstxamarindroid.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Duration { get; set; }
        
        public SongModel()
        {

        }

        public SongModel(int id, string name, int duration)
        {
            this.Id         = id;
            this.Name       = name;
            this.Duration   = duration;
        }
    }
}
