using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;

namespace firstxamarindroid.ViewHolders
{
    public class SongViewHolder : RecyclerView.ViewHolder
    {
        [InjectView(Resource.Id.textViewOrderNr)]
        public TextView textViewOrderNr;

        [InjectView(Resource.Id.textViewSongName)]
        public TextView textViewSongName;

        [InjectView(Resource.Id.textViewSongDuration)]
        public TextView textViewSongDuration;

        [InjectView(Resource.Id.imageViewPlay)]
        public ImageView imageViewPlay;


        public SongViewHolder(View view) : base(view)
        {
            Cheeseknife.Inject(this, view);
        }
    }
}
