using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using firstxamarindroid.Models;
using firstxamarindroid.ViewHolders;

namespace firstxamarindroid.Adapters
{
    public class HeaterSensorsAdapter : RecyclerView.Adapter
    {
        // List which holds heater sensors data passed from constructor
        private List<HeaterSensorModel> heaterSensorsList;


        public HeaterSensorsAdapter(List<HeaterSensorModel> heaterSensorsList)
        {
            this.heaterSensorsList = heaterSensorsList;
        }


        /// <summary>
        /// Method which returns items count to be shown in recyclerview
        /// </summary>
        public override int ItemCount => this.heaterSensorsList.Count;


        /// <summary>
        /// Method which binds data from models to views based on position
        /// 
        /// </summary>
        /// <param name="holder">Custom ViewHolder</param>
        /// <param name="position">Position on which recyclerview is</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            HeaterSensorModel heaterSensorModel = this.heaterSensorsList[position];

            HeaterSensorViewHolder heaterSensorViewHolder = (HeaterSensorViewHolder)holder;
            heaterSensorViewHolder.textViewSensorName.Text  = heaterSensorModel.Name;
            heaterSensorViewHolder.textViewGrades.Text      = heaterSensorModel.Temperature.ToString();

            heaterSensorViewHolder.UpdateCircularGauge((int)heaterSensorModel.Temperature);
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
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_sensor_layout, parent, false);

            return new HeaterSensorViewHolder(view);
        }
    }
}
