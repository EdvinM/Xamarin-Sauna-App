
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
using firstxamarindroid.Helpers;
using firstxamarindroid.Models;
using Net.ArcanaStudio.ColorPicker;
using Realms;

namespace firstxamarindroid.Fragments
{
    public class LightSettingsFragment : Fragment
    {
        // Injects views to variables
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

        [InjectView(Resource.Id.textViewTapForColor)]
        private TextView textViewTapForColor;

        // Other variable declarations
        private LightModel lightModel;

        private ToggleButtons toggleButtons;



        public static LightSettingsFragment NewInstance(int saunaId, String lightId)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt(Helpers.Helpers.ARG_1, saunaId);
            bundle.PutString(Helpers.Helpers.ARG_2, lightId);

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
                int saunaId = Arguments.GetInt(Helpers.Helpers.ARG_1);
                String lightId = Arguments.GetString(Helpers.Helpers.ARG_2);

                this.lightModel = DbController.Instance.GetLight(saunaId, lightId);
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
            Log.Debug("LightSettings", "Debug message 2");

            this.toggleButtons = new ToggleButtons(view);
            this.toggleButtons.UpdateToggleButtons(this.lightModel.Status);
            this.toggleButtons.OnToggleChanged += ToggleButtons_OnToggleChanged;

            if ((!this.lightModel.Status && this.lightModel.ColorStatus) || this.lightModel.Status)
            {
                Realm.GetInstance().Write(() => this.lightModel.Status = true);

                Log.Debug("LightSettings", "Debug message 1");

                this.toggleButtons.UpdateToggleButtons(this.lightModel.Status);

                this.switchColorLightOn.Checked = this.lightModel.ColorStatus;

                if (this.lightModel.ColorStatus)
                {
                    this.colorPanelView.Visibility = ViewStates.Visible;
                    this.textViewTapForColor.Visibility = ViewStates.Visible;

                    this.colorPanelView.SetColor(this.lightModel.GetColor);
                }

                // Set visible brightness layout
                this.linearLayoutBrightness.Visibility = ViewStates.Visible;

                // Also set the layout brightness values
                this.seekBarBrightness.Progress = this.lightModel.Brightness;
                this.textViewBrightnessLevel.Text = "Brightness: " + this.lightModel.Brightness;
            }

            this.seekBarBrightness.ProgressChanged += SeekBarBrightness_ProgressChanged;
        }





        /// <summary>
        /// Method which is called from parent activity, in order to update layout and selected color from dialog picker
        /// </summary>
        /// <param name="color">Selected color</param>
        public void UpdateSelectedColor(Color color)
        {
            this.colorPanelView.SetColor(color);

            // Update and save light model color.
            this.lightModel.UpdateColor(color);
        }

        /// <summary>
        /// Callback for progress changed on seekbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeekBarBrightness_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            Realm.GetInstance().Write(() => this.lightModel.Brightness = e.Progress);

            this.textViewBrightnessLevel.Text = "Brightness: " + this.lightModel.Brightness;
        }





        [InjectOnCheckedChange(Resource.Id.switchColorLightOn)]
        void OnColorLightCheckedListener(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Realm.GetInstance().Write(() => this.lightModel.ColorStatus = e.IsChecked);

            this.colorPanelView.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
            this.textViewTapForColor.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
        }

        private void ToggleButtons_OnToggleChanged(object sender, bool e)
        {
            Realm.GetInstance().Write(() => this.lightModel.Status = e);

            this.linearLayoutBrightness.Visibility = e ? ViewStates.Visible : ViewStates.Gone;

            // Also hide or show the color panel view based on previous light settings.
            if (!e)
            {
                this.colorPanelView.Visibility = ViewStates.Gone;
                this.textViewTapForColor.Visibility = ViewStates.Gone;
            }
            else
            {
                if (this.lightModel.ColorStatus)
                {
                    this.colorPanelView.Visibility = ViewStates.Visible;
                    this.textViewTapForColor.Visibility = ViewStates.Visible;
                }
            }
        }

        // Click listener for color picker element
        [InjectOnClick(Resource.Id.colorPanelView1)]
        void OnColorPanelClickListener(object sender, EventArgs e)
        {
            ColorPickerDialog.NewBuilder().SetColor(Color.Aqua).Show(this.Activity);
        }
    }
}
