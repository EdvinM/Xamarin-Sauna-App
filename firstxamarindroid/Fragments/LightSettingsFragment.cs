
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
using Android.Views.Animations;
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
        /*
         ************************************
         *      Bind Views to Variables
         ************************************
         */

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


        /*
         ************************************
         *      Declare Variables
         ************************************
         */
        private LightModel lightModel;
        private SaunaModel saunaModel;

        private ToggleButtons toggleButtons;


        /*
         ************************************
         *      Fragment methods
         ************************************
         */

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
                this.saunaModel = DbController.Instance.GetSauna(saunaId);
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
            this.toggleButtons = new ToggleButtons(view, this.saunaModel);
            this.toggleButtons.UpdateToggleButtons(this.lightModel.Status);
            this.toggleButtons.OnToggleChanged += ToggleButtons_OnToggleChanged;

            // Update color switch, based on saved settings
            this.switchColorLightOn.Checked = this.lightModel.ColorStatus;

            // Set selected color for panel view
            this.colorPanelView.SetColor(this.lightModel.GetColor);

            // Update color layout visibilities based on color status
            if (this.lightModel.ColorStatus)
            {
                this.colorPanelView.Visibility      = ViewStates.Visible;
                this.textViewTapForColor.Visibility = ViewStates.Visible;
            }

            // Update layout if light status is on
            if (this.lightModel.Status)
            {
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



        /*
         ************************************
         *      Callbacks
         ************************************
         */

        [InjectOnCheckedChange(Resource.Id.switchColorLightOn)]
        void OnColorLightCheckedListener(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Realm.GetInstance().Write(() => this.lightModel.ColorStatus = e.IsChecked);

            this.colorPanelView.Visibility      = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
            this.textViewTapForColor.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;

            // Start animation only if switch is on.
            if (e.IsChecked)
            {
                this.colorPanelView.StartAnimation(AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.fade_in));
                this.textViewTapForColor.StartAnimation(AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.fade_in));
            }
        }

        private void ToggleButtons_OnToggleChanged(object sender, bool e)
        {
            Realm.GetInstance().Write(() => this.lightModel.Status = e);

            // Start linear layout animation only when showing the layout
            if (e)
            {
                this.linearLayoutBrightness.StartAnimation(AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.fade_in));
            }

            this.linearLayoutBrightness.Visibility = e ? ViewStates.Visible : ViewStates.Gone;

            // Also hide or show the color panel view based on previous light settings.
            if (!e && !this.lightModel.ColorStatus)
            {
                this.colorPanelView.Visibility      = ViewStates.Gone;
                this.textViewTapForColor.Visibility = ViewStates.Gone;
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
