using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;

namespace firstxamarindroid.Helpers
{
    public class LightItemViewHolder : RecyclerView.ViewHolder
    {
        [InjectView(Resource.Id.textViewLightName)]
        public TextView textViewLightName;

        [InjectView(Resource.Id.imageViewLightStatus)]
        public ImageView imageViewLightStatus;

        [InjectView(Resource.Id.linearLayoutLampMain)]
        public LinearLayout linearLayoutLampMain;

        public LightItemViewHolder(View view) : base(view)
        {
            Cheeseknife.Inject(this, view);
        }
    }
}
