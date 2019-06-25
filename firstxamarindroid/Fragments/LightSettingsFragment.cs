﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.Models;
using Net.ArcanaStudio.ColorPicker;

namespace firstxamarindroid.Fragments
{
    public class LightSettingsFragment : Fragment
    {
        // Class constants
        private const String ARG_1 = "ARG_1";


        // Injects views to variables
        [InjectView(Resource.Id.switchLightOn)]
        private Switch switchLightOn;

        [InjectView(Resource.Id.linearLayoutBrightness)]
        private LinearLayout linearLayoutBrightness;

        [InjectView(Resource.Id.seekBarBrightness)]
        private SeekBar seekBarBrightness;

        [InjectView(Resource.Id.textViewBrightnessLevel)]
        private TextView textViewBrightnessLevel;

        [InjectView(Resource.Id.switchColorLightOn)]
        private Switch switchColorLightOn;

        [InjectView(Resource.Id.colorPanelView1)]
        private ColorPanelView colorPanelView;

        // Other variable declarations
        private LightModel lightModel;


        public static LightSettingsFragment NewInstance(LightModel lightModel)
        {
            Bundle bundle = new Bundle();
            bundle.PutSerializable(ARG_1, lightModel);

            LightSettingsFragment lightSettingsFragment = new LightSettingsFragment();
            lightSettingsFragment.Arguments = bundle;

            return lightSettingsFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Get model serializable
            if(Arguments != null)
            {
                this.lightModel = (LightModel)Arguments.GetSerializable(ARG_1);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.light_settings_fragment, container, false);

            Cheeseknife.Inject(this, view);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // At first, set all values acording to model values
            if ((!this.lightModel.Status && this.lightModel.ColorStatus) || this.lightModel.Status)
            {
                this.lightModel.Status = true;
                this.switchLightOn.Checked = this.lightModel.Status;
                this.switchColorLightOn.Checked = this.lightModel.ColorStatus;

                if (this.lightModel.ColorStatus)
                    this.colorPanelView.Visibility = ViewStates.Visible;

                // Set visible brightness layout
                this.linearLayoutBrightness.Visibility = ViewStates.Visible;

                // Also set the layout brightness values
                this.seekBarBrightness.Progress = this.lightModel.Brightness;
                this.textViewBrightnessLevel.Text = "Brightness: " + this.lightModel.Brightness;
            }

            this.seekBarBrightness.ProgressChanged += SeekBarBrightness_ProgressChanged;
        }

        [InjectOnCheckedChange(Resource.Id.switchColorLightOn)]
        void OnColorLightCheckedListener(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            this.lightModel.ColorStatus = e.IsChecked;

            this.colorPanelView.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
        }

        [InjectOnCheckedChange(Resource.Id.switchLightOn)]
        void OnSwitchCheckedListener(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            this.lightModel.Status = e.IsChecked;

            this.linearLayoutBrightness.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;

            // Also hide or show the color panel view based on previous light settings.
            if (!e.IsChecked)
                this.colorPanelView.Visibility = ViewStates.Gone;
            else
            {
                if (this.lightModel.ColorStatus)
                    this.colorPanelView.Visibility = ViewStates.Visible;
            }
        }

        // Click listener for color picker element
        [InjectOnClick(Resource.Id.colorPanelView1)]
        void OnColorPanelClickListener(object sender, EventArgs e)
        {
            ColorPickerDialog.NewBuilder().SetColor(Color.Aqua).Show(this.Activity);
        }

        /// <summary>
        /// Method which is called from parent activity, in order to update layout and selected color from dialog picker
        /// </summary>
        /// <param name="color">Selected color</param>
        public void UpdateSelectedColor(Color color)
        {
            this.colorPanelView.SetColor(color);
        }

        /// <summary>
        /// Callback for progress changed on seekbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeekBarBrightness_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            this.lightModel.Brightness = e.Progress;

            this.textViewBrightnessLevel.Text = "Brightness: " + this.lightModel.Brightness;
        }
    }
}