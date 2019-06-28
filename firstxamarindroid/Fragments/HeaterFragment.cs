
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Flexbox;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.Adapters;
using firstxamarindroid.Helpers;
using firstxamarindroid.Models;
using Realms;
using Syncfusion.Android.Buttons;

namespace firstxamarindroid.SettingsModule
{
    public class HeaterFragment : Fragment
    {
        [InjectView(Resource.Id.toggleHeater)]
        private Switch toggleHeater;

        [InjectView(Resource.Id.sfSegmentedControlPower)]
        SfSegmentedControl sfSegmentedControlPower;

        [InjectView(Resource.Id.editTextTemperature)]
        EditText editTextTemperature;

        [InjectView(Resource.Id.buttonUpdateTemp)]
        Button buttonUpdateTemp;

        [InjectView(Resource.Id.recyclerViewSensorTemperatures)]
        RecyclerView recyclerViewSensorTemperatures;

        private SaunaModel saunaModel;
        private HeaterModel heaterModel;

        private List<String> heterModelList = new List<String>()
        {
            "LOW","MEDIUM","HIGH"
        };

        private List<HeaterSensorModel> heaterSensorModels = new List<HeaterSensorModel>()
        {
            new HeaterSensorModel(1, "Sensor 1", 13.4),
            new HeaterSensorModel(2, "Sensor 2", 20.0),
            new HeaterSensorModel(3, "Sensor 3", 93.4)
        };

        private HeaterSensorsAdapter heaterSensorsAdapter;



        public static HeaterFragment NewInstance(int saunaId)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt(Helpers.Helpers.ARG_1, saunaId);

            HeaterFragment heaterFragment = new HeaterFragment();
            heaterFragment.Arguments = bundle;

            return heaterFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                int saunaId = Arguments.GetInt(Helpers.Helpers.ARG_1);

                this.saunaModel = DbController.Instance.GetSauna(saunaId);
                this.heaterModel = this.saunaModel.Heater;

                // Create heater sensors adapter
                this.heaterSensorsAdapter = new HeaterSensorsAdapter(this.heaterSensorModels);
            }
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.heater_fragment, container, false);

            Cheeseknife.Inject(this, view);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // Set heater value
            this.toggleHeater.Checked = this.heaterModel.Status;

            // Set segmented control power items, selection event and selected index
            this.sfSegmentedControlPower.DisplayMode = SegmentDisplayMode.Text;
            this.sfSegmentedControlPower.SegmentHeight = 25;
            this.sfSegmentedControlPower.SelectionTextColor = Color.LimeGreen;
            this.sfSegmentedControlPower.FontColor = Color.White;
            this.sfSegmentedControlPower.CornerRadius = 15;
            this.sfSegmentedControlPower.BackColor = Color.LightGreen;
            this.sfSegmentedControlPower.BorderColor = Color.DarkGreen;
            this.sfSegmentedControlPower.BorderThickness = 5;
            this.sfSegmentedControlPower.VisibleSegmentsCount = 3;
            this.sfSegmentedControlPower.ItemsSource = this.heterModelList;
            this.sfSegmentedControlPower.SelectedIndex = GetSelectedHeatingIndex(this.heterModelList, this.heaterModel);
            SelectionIndicatorSettings selectionIndicator = new SelectionIndicatorSettings();
            selectionIndicator.Color = Color.White;

            this.sfSegmentedControlPower.SelectionIndicatorSettings = selectionIndicator;

            this.sfSegmentedControlPower.SelectionChanged += SfSegmentedControlPower_SelectionChanged;

            // Update selected edit temperature
            this.editTextTemperature.Text = this.heaterModel.TemperatureThreshold.ToString();

            // Set values to heater sensors and recyclerview
            this.recyclerViewSensorTemperatures.SetAdapter(this.heaterSensorsAdapter);

            /*
             * Special type of layout manager for recycler view, developed by google: FlexLayoutManager
             * https://github.com/AigioL/XAB.FlexboxLayout
             *
             * Let's easily manage items when not using linearlayout manager.
             */

            FlexboxLayoutManager flexboxLayoutManager = new FlexboxLayoutManager(this.Activity, FlexDirection.Row, FlexWrap.Wrap);
            flexboxLayoutManager.JustifyContent = JustifyContent.Center;
            flexboxLayoutManager.AlignItems = AlignItems.Stretch;
            flexboxLayoutManager.FlexWrap = FlexWrap.Wrap;


            this.recyclerViewSensorTemperatures.SetLayoutManager(flexboxLayoutManager);
            this.recyclerViewSensorTemperatures.NestedScrollingEnabled = false;
        }




        private int GetSelectedHeatingIndex(List<String> items, HeaterModel heaterModel)
        {
            for (int i = 0; i < items.Count(); i++)
                if (heaterModel.Power.Equals(items[i]))
                    return i;

            return 0;
        }




        [InjectOnCheckedChange(Resource.Id.toggleHeater)]
        void OnHeater_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Realm.GetInstance().Write(() => this.heaterModel.Status = e.IsChecked);

            Toast.MakeText(this.Activity, "Heater " + ((e.IsChecked) ? "On" : "Off"), ToastLength.Short).Show();
        }

        [InjectOnClick(Resource.Id.buttonUpdateTemp)]
        private void ButtonUpdateTemp_Click(object sender, EventArgs e)
        {
            Realm.GetInstance().Write(() => this.heaterModel.TemperatureThreshold = int.Parse(this.editTextTemperature.Text));

            Toast.MakeText(this.Activity, "Threshold temperature updated.", ToastLength.Short).Show();
        }

        private void SfSegmentedControlPower_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Realm.GetInstance().Write(() => this.heaterModel.Power = this.heterModelList[e.Index]);

            Toast.MakeText(this.Activity, "Heater power updated.", ToastLength.Short).Show();
        }
    }
}
