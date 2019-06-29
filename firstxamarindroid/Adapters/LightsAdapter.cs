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
        // List with light models data.
        private List<LightModel> lightModelList;
        private Context context;

        // Event to handle lamp item click listener;
        public event EventHandler<LightModel> OnItemClick;

        public LightsAdapter(Context context, List<LightModel> lightModelList)
        {
            this.lightModelList = lightModelList;
            this.context        = context;
        }


        /// <summary>
        /// Method which returns items count to be shown in recyclerview
        /// </summary>
        public override int ItemCount => this.lightModelList.Count;


        /// <summary>
        /// Method which binds data from models to views based on position
        /// 
        /// </summary>
        /// <param name="holder">Custom ViewHolder</param>
        /// <param name="position">Position on which recyclerview is</param>
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


        /// <summary>
        /// Method on which we specified layout and view holder on which this recyclerview should run
        /// 
        /// </summary>
        /// <param name="parent">View group parent</param>
        /// <param name="viewType">Returned viewtype from getviewtype method of recyclerview adapter method</param>
        /// <returns></returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.light_item_layout, parent, false);

            return new LightItemViewHolder(view);
        }
    }
}
