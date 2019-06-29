
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.Helpers;
using firstxamarindroid.Models;
using Java.Lang;
using Realms;

namespace firstxamarindroid.SettingsModule
{
    public class VentilationFragment : Fragment
    {
        /*
         ************************************
         *      Bind Views to Variables
         ************************************
         */

        [InjectView(Resource.Id.linearLayoutProgress)]
        private LinearLayout linearLayoutProgress;

        [InjectView(Resource.Id.progressBarOpening)]
        private ProgressBar progressBarOpening;

        [InjectView(Resource.Id.textViewProgressStatus)]
        private TextView textViewProgressStatus;

        [InjectView(Resource.Id.editTextFanDuration1)]
        private EditText editTextFanDuration1;

        [InjectView(Resource.Id.editTextFanDuration2)]
        private EditText editTextFanDuration2;

        [InjectView(Resource.Id.toggleButtonFan1)]
        private ToggleButton toggleButtonFan1;

        [InjectView(Resource.Id.toggleButtonFan2)]
        private ToggleButton toggleButtonFan2;


        /*
         ************************************
         *      Declare Variables
         ************************************
         */
        private SaunaModel saunaModel;
        private VentilationModel ventilationModel;

        private Timer timer;

        private ToggleButtons toggleButtons;

        /*
         *********************************
         *      Fragment methods
         *********************************
         */

        public static VentilationFragment NewInstance(int saunaId)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt(Helpers.Helpers.ARG_1, saunaId);

            VentilationFragment ventilationFragment = new VentilationFragment();
            ventilationFragment.Arguments = bundle;

            return ventilationFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                int saunaId = Arguments.GetInt(Helpers.Helpers.ARG_1);

                this.saunaModel = DbController.Instance.GetSauna(saunaId);
                this.ventilationModel = this.saunaModel.Ventilation;

            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.ventilation_fragment, container, false);

            Cheeseknife.Inject(this, view);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            this.toggleButtons = new ToggleButtons(view, this.saunaModel);
            this.toggleButtons.UpdateToggleButtons(this.ventilationModel.Status);
            this.toggleButtons.OnToggleChanged += ToggleButtons_OnToggleChanged;


            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() < this.ventilationModel.OpenedAt)
            {
                this.linearLayoutProgress.Visibility = ViewStates.Visible;

                int diff = (int)(this.ventilationModel.OpenedAt - DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                this.progressBarOpening.Progress = 150 - diff;
                this.textViewProgressStatus.Text = System.Math.Ceiling(100 - ((diff * 100) / 150.0)) + " %";
            }

            editTextFanDuration1.Text = this.ventilationModel.FanRunningTime1.ToString();
            editTextFanDuration2.Text = this.ventilationModel.FanRunningTime2.ToString();
            
            toggleButtonFan1.Checked = DateTimeOffset.UtcNow.ToUnixTimeSeconds() < this.ventilationModel.Fan1ClosingAt;
            toggleButtonFan2.Checked = DateTimeOffset.UtcNow.ToUnixTimeSeconds() < this.ventilationModel.Fan2ClosingAt;


            // Create a timer which should update the views, when time runs out and ventilation completes to updated
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1005;
            timer.Elapsed += (sender, e) => { Timer_Elapsed(); };
            timer.Start();
        }

        public override void OnStop()
        {
            timer.Dispose();

            base.OnStop();
        }


        /*
         ****************************************************
         *      Callbacks & UI elements listeners
         ****************************************************
         */

        [InjectOnCheckedChange(Resource.Id.toggleButtonFan1)]
        void OnToggleButton1CheckedListener(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            // Open fan only if ventilation is opened
            if (!this.ventilationModel.Status)
            {
                Toast.MakeText(this.Activity, "Please open ventilation first, then you can open this fan.", ToastLength.Short).Show();
                return;
            }

            ToggleButton toggleButtonSender = (ToggleButton)sender;

            if (toggleButtonSender == toggleButtonFan1 && this.editTextFanDuration1.Text.Length <= 0)
            {
                Toast.MakeText(this.Activity, "Fan durations cannot be empty", ToastLength.Short).Show();
                return;
            }

            if (toggleButtonSender == toggleButtonFan2 && this.editTextFanDuration2.Text.Length <= 0)
            {
                Toast.MakeText(this.Activity, "Fan durations cannot be empty", ToastLength.Short).Show();
                return;
            }

            // Get fan duration seconds
            int secondsDuration = (toggleButtonSender == toggleButtonFan1 ? Convert.ToInt16(editTextFanDuration1.Text) : Convert.ToInt16(editTextFanDuration2.Text));

            // Calculate when the fan should close
            long closingAt = e.IsChecked ? (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + secondsDuration) : -1;

            Toast.MakeText(this.Activity, e.IsChecked ? "Fan opened successfully" : "Fan closed successfully", ToastLength.Short).Show();

            Realm.GetInstance().Write(() =>
            {
                if (toggleButtonSender == toggleButtonFan1)
                {
                    Log.Debug("VentilationFrag", "Closing at 1= " + closingAt);
                    this.ventilationModel.Fan1ClosingAt = closingAt;
                }
                else
                {
                    Log.Debug("VentilationFrag", "Closing at 2= " + closingAt);
                    this.ventilationModel.Fan2ClosingAt = closingAt;
                }
            });
        }

        [InjectOnCheckedChange(Resource.Id.toggleButtonFan2)]
        void OnToggleButton2CheckedListener(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            OnToggleButton1CheckedListener(sender, e);
        }

        [InjectOnTextChanged(Resource.Id.editTextFanDuration1)]
        void OnFan1_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (e.Text.ToString().Length <= 0)
                return;

            Realm.GetInstance().Write(() => this.ventilationModel.FanRunningTime1 = int.Parse(e.Text.ToString()));
        }

        [InjectOnTextChanged(Resource.Id.editTextFanDuration2)]
        void OnFan2_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (e.Text.ToString().Length <= 0)
                return;

            Realm.GetInstance().Write(() => this.ventilationModel.FanRunningTime2 = int.Parse(e.Text.ToString()));
        }

        void ToggleButtons_OnToggleChanged(object sender, bool e)
        {
            // This is important to be set to true, because we don't want to call code from toggle checked function if user did not
            // press this toggle ventilation.
            Log.Debug("VentilationFragment", "Clicked listener");

            this.linearLayoutProgress.Visibility = e ? ViewStates.Visible : ViewStates.Gone;

            if (e)
            {
                Realm.GetInstance().Write(() =>
                {
                    this.ventilationModel.OpenedAt = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 150);
                    this.ventilationModel.Status = e;
                });
            }
            else
            {
                Realm.GetInstance().Write(() =>
                {
                    this.ventilationModel.Status = e;
                    this.ventilationModel.OpenedAt = -1;
                });

                this.progressBarOpening.Progress = 0;
                this.textViewProgressStatus.Text = "0%";
            }
        }



        /*
         ****************************************************
         *                  Other methods
         ****************************************************
         */

        private void Timer_Elapsed()
        {
            // Should run on Ui Thread.
            this.Activity.RunOnUiThread(() =>
            {
                if (this.linearLayoutProgress.Visibility == ViewStates.Visible)
                {
                    if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > this.ventilationModel.OpenedAt)
                    {
                        this.linearLayoutProgress.Visibility = ViewStates.Gone;

                        this.progressBarOpening.Progress = 0;
                        this.textViewProgressStatus.Text = "0%";
                    }
                    else
                    {
                        int diff = (int)(this.ventilationModel.OpenedAt - DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                        this.progressBarOpening.Progress = 150 - diff;
                        this.textViewProgressStatus.Text = System.Math.Ceiling(100 - ((diff * 100) / 150.0)) + " %";
                    }
                }


                // If current time is greater than fan closing time, close the fan
                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > this.ventilationModel.Fan1ClosingAt && toggleButtonFan1.Checked)
                {
                    toggleButtonFan1.Checked = false;
                }

                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > this.ventilationModel.Fan2ClosingAt && toggleButtonFan2.Checked)
                {
                    toggleButtonFan2.Checked = false;
                }
            });
        }
    }
}
