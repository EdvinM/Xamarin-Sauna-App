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

        public override int ItemCount => this.songsList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SongModel songModel = this.songsList[position];

            SongViewHolder songViewHolder = (SongViewHolder)holder;
            songViewHolder.textViewOrderNr.Text = (position + 1).ToString();
            songViewHolder.textViewSongName.Text = songModel.Name;
            songViewHolder.textViewSongDuration.Text = TimeSpan.FromSeconds(songModel.Duration).ToString(@"hh\:mm\:ss");
            songViewHolder.imageViewPlay.Click += ImageViewPlay_Click;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_song_layout, parent, false);

            return new SongViewHolder(view);
        }


        private void ImageViewPlay_Click(object sender, EventArgs e)
        {
            // Play selected song.
            Log.Debug("SongsAdapter", "Song played");
        }
    }
}
