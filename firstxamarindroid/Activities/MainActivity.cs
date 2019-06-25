﻿using System;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.App;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.SettingsModule;
using Net.ArcanaStudio.ColorPicker;
using firstxamarindroid.Fragments;

namespace firstxamarindroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IColorPickerDialogListener
    {
        [InjectView(Resource.Id.toolbar)]
        private Android.Support.V7.Widget.Toolbar toolbar;

        [InjectView(Resource.Id.textViewSaunaNameSettings)]
        private TextView textViewSaunaNameSettings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Initialize cheeseknife
            Cheeseknife.Inject(this);

            // Set actionbar
            SetSupportActionBar(toolbar);

            textViewSaunaNameSettings.Text = "Sauna 1";

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame_layout_settings, new SaunaSettingsFragment()).Commit();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //int id = item.ItemId;
            //if (id == Resource.Id.action_settings)
            //{
            //    return true;
            //}

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnColorSelected(int dialogId, Color color)
        {
            // Get shown fragment instance from support fragment manager
            Android.Support.V4.App.Fragment currentFragment = SupportFragmentManager.FindFragmentById(Resource.Id.frame_layout_settings);

            // Check if the current displayed fragment is of type light settings.
            if (currentFragment is LightSettingsFragment && currentFragment.IsVisible)
            {
                // Send & update selected color.
                ((LightSettingsFragment)currentFragment).UpdateSelectedColor(color);
            }
        }

        public void OnDialogDismissed(int dialogId) { }
    }
}

