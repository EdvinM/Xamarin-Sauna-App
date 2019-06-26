
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
using Realms;

namespace firstxamarindroid.SettingsModule
{
    public class VentilationFragment : Fragment
    {
        [InjectView(Resource.Id.toggleVentilation)]
        private Switch toggleVentilation;

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


        private SaunaModel saunaModel;
        private VentilationModel ventilationModel;

        private Timer timer;


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

            this.toggleVentilation.Checked = this.ventilationModel.Status;

            Log.Debug("VentilationFragment", "Opened at= " + this.ventilationModel.OpenedAt);
            Log.Debug("VentilationFragment", "Timestamp at= " + DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() < this.ventilationModel.OpenedAt)
            {
                this.linearLayoutProgress.Visibility = ViewStates.Visible;

                int diff = (int)(this.ventilationModel.OpenedAt - DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                this.progressBarOpening.Progress = 150 - diff;
                this.textViewProgressStatus.Text = Math.Ceiling(100 - ((diff * 100) / 150.0)) + " %";
            }

            editTextFanDuration1.Text = this.ventilationModel.FanRunningTime1.ToString();
            editTextFanDuration2.Text = this.ventilationModel.FanRunningTime2.ToString();

            toggleButtonFan1.Checked = DateTimeOffset.UtcNow.ToUnixTimeSeconds() < this.ventilationModel.Fan1ClosingAt;
            toggleButtonFan2.Checked = DateTimeOffset.UtcNow.ToUnixTimeSeconds() < this.ventilationModel.Fan2ClosingAt;


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




        [InjectOnCheckedChange(Resource.Id.toggleVentilation)]
        void OnColorLightCheckedListener(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            this.linearLayoutProgress.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;

            if (e.IsChecked)
            {
                Realm.GetInstance().Write(() => {
                    this.ventilationModel.OpenedAt = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 150);
                    this.ventilationModel.Status = e.IsChecked;
                });
            }
            else
            {
                Realm.GetInstance().Write(() => {
                    this.ventilationModel.Status = e.IsChecked;
                    this.ventilationModel.OpenedAt = -1;
                });
            }
        }





        private void Timer_Elapsed()
        {
            Log.Debug("VentilationFragment", "TImer callback called");

            //timer.Stop();

            this.Activity.RunOnUiThread(() =>
            {
                if (this.linearLayoutProgress.Visibility == ViewStates.Visible)
                {
                    Log.Debug("VentilationFragment", "TImer callback called - visibleeee");
                    Log.Debug("VentilationFragment", "Debug this this");
                    Log.Debug("VentilationFragment", "Opened at= " + this.ventilationModel.OpenedAt.ToString());
                    //Log.Debug("VentilationFragment", "Timestamp at= " + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
                    Log.Debug("VentilationFragment", "Debug this this - 2");

                    if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > this.ventilationModel.OpenedAt)
                    {
                        this.linearLayoutProgress.Visibility = ViewStates.Gone;

                        this.progressBarOpening.Progress = 0;
                        this.textViewProgressStatus.Text = "0%";

                        Log.Debug("VentilationFragment", "Debug this this");
                    }
                    else
                    {
                        int diff = (int)(this.ventilationModel.OpenedAt - DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                        Log.Debug("VentilationFragment", "Progress updated");

                        this.progressBarOpening.Progress = 150 - diff;
                        this.textViewProgressStatus.Text = Math.Ceiling(100 - ((diff * 100) / 150.0)) + " %";
                    }
                }


                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > this.ventilationModel.Fan1ClosingAt && toggleButtonFan1.Checked)
                {
                    toggleButtonFan1.Checked = false;
                }

                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > this.ventilationModel.Fan2ClosingAt && toggleButtonFan2.Checked)
                {
                    toggleButtonFan2.Checked = false;
                }
            });

            //timer.Dispose();
        }
    }
}
