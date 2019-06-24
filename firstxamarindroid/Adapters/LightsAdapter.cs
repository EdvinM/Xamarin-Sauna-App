using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using firstxamarindroid.Helpers;
using firstxamarindroid.Models;

namespace firstxamarindroid.Adapters
{
    public class LightsAdapter : RecyclerView.Adapter
    {
        private List<LightModel> lightModelList;
        private Context context;

        // Event to handle lamp click listener;
        public event EventHandler<LightModel> OnItemClick;

        public LightsAdapter(Context context, List<LightModel> lightModelList)
        {
            this.lightModelList = lightModelList;
            this.context        = context;
        }

        public override int ItemCount => this.lightModelList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            LightModel lightModel = this.lightModelList[position];


            LightItemViewHolder lightItemViewHolder = (LightItemViewHolder)holder;
            lightItemViewHolder.textViewLightName.Text = lightModel.Name;

            // Set main layout click listener and delegate it to the event.
            lightItemViewHolder.linearLayoutLampMain.Click += (sender, e) => OnItemClick(this, this.lightModelList[position]);

            // Change the lamp icon based on it's status (if it's enabled or not).
            Glide.With(this.context).Load(lightModel.Status ? Resource.Mipmap.ic_lamp_on : Resource.Mipmap.ic_lamp_off).Apply(RequestOptions.OverrideOf(60, 60)).Into(lightItemViewHolder.imageViewLightStatus);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.light_item_layout, parent, false);

            return new LightItemViewHolder(view);
        }
    }
}
