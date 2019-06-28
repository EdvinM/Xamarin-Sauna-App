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
        private List<HeaterSensorModel> heaterSensorsList;


        public HeaterSensorsAdapter(List<HeaterSensorModel> heaterSensorsList)
        {
            this.heaterSensorsList = heaterSensorsList;
        }

        public override int ItemCount => this.heaterSensorsList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            HeaterSensorModel heaterSensorModel = this.heaterSensorsList[position];

            HeaterSensorViewHolder heaterSensorViewHolder = (HeaterSensorViewHolder)holder;
            heaterSensorViewHolder.textViewSensorName.Text = heaterSensorModel.Name;
            heaterSensorViewHolder.textViewGrades.Text = heaterSensorModel.Temperature.ToString();

            heaterSensorViewHolder.UpdateCircularGauge((int)heaterSensorModel.Temperature);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_sensor_layout, parent, false);

            return new HeaterSensorViewHolder(view);
        }
    }
}
