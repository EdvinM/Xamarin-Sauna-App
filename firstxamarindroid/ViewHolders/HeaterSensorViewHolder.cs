using System;
using System.Collections.ObjectModel;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using Com.Syncfusion.Gauges.SfCircularGauge;

namespace firstxamarindroid.ViewHolders
{
    public class HeaterSensorViewHolder : RecyclerView.ViewHolder
    {
        [InjectView(Resource.Id.sfCircularGaugeTemperature)]
        public SfCircularGauge sfCircularGaugeTemperature;

        [InjectView(Resource.Id.textViewSensorName)]
        public TextView textViewSensorName;

        [InjectView(Resource.Id.textViewGrades)]
        public TextView textViewGrades;

        public HeaterSensorViewHolder(View view) :base(view)
        {
            Cheeseknife.Inject(this, view);
        }


        public void UpdateCircularGauge(int temperature)
        {
            ObservableCollection<CircularScale> scales = new ObservableCollection<CircularScale>();
            CircularScale scale = new CircularScale();
            scale.StartValue = 0;
            scale.EndValue = 100;
            NeedlePointer needlePointer = new NeedlePointer();
            needlePointer.Value = temperature;
            needlePointer.KnobRadius = 15;
            needlePointer.TailColor = Color.ParseColor("#757575");
            needlePointer.TailLengthFactor = 0.2;
            needlePointer.TailStrokeWidth = 1;
            needlePointer.TailStrokeColor = Color.ParseColor("#757575");
            scale.CircularPointers.Add(needlePointer);
            scales.Add(scale);

            sfCircularGaugeTemperature.CircularScales = scales;
        }
    }
}
