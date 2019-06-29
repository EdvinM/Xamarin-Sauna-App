using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using firstxamarindroid.Models;
using firstxamarindroid.ViewHolders;

namespace firstxamarindroid.Adapters
{
    public class SongsAdapter : RecyclerView.Adapter
    {
        private List<SongModel> songsList;

        public SongsAdapter(List<SongModel> songsList)
        {
            this.songsList = songsList;
        }


        /// <summary>
        /// Method which returns items count to be shown in recyclerview
        /// </summary>
        public override int ItemCount => this.songsList.Count;


        /// <summary>
        /// Method which binds data from models to views based on position
        /// 
        /// </summary>
        /// <param name="holder">Custom ViewHolder</param>
        /// <param name="position">Position on which recyclerview is</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SongModel songModel = this.songsList[position];

            SongViewHolder songViewHolder = (SongViewHolder)holder;

            songViewHolder.textViewOrderNr.Text         = (position + 1).ToString();
            songViewHolder.textViewSongName.Text        = songModel.Name;
            songViewHolder.textViewSongDuration.Text    = TimeSpan.FromSeconds(songModel.Duration).ToString(@"hh\:mm\:ss");

            songViewHolder.imageViewPlay.Click          += ImageViewPlay_Click;
        }


        /// <summary>
        /// Method on which we specified layout and view holder on which this recyclerview should run
        /// 
        /// </summary>
        /// <param name="parent">View group parent</param>
        /// <param name="viewType">Returned viewtype from getviewtype method of recyclerview adapter method</param>
        /// <returns>Initialized HeaterSensorViewHolder</returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_song_layout, parent, false);

            return new SongViewHolder(view);
        }


        /// <summary>
        /// Callback for song's image view click listener. Called when we had to change the music in sauna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageViewPlay_Click(object sender, EventArgs e)
        {
            // Play selected song.
            Log.Debug("SongsAdapter", "Song played");
        }
    }
}
