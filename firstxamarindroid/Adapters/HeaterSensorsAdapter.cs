using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using firstxamarindroid.Models;

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
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}
