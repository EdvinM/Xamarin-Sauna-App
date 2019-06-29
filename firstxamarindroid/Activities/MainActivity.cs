using System;
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
    [Activity(Label = "Sauna Settings", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, IColorPickerDialogListener
    {
        [InjectView(Resource.Id.toolbar)]
        private Android.Support.V7.Widget.Toolbar toolbar;

        [InjectView(Resource.Id.textViewSaunaNameSettings)]
        private TextView textViewSaunaNameSettings;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Initialize xamarin platform essentials
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Register syncfusion products
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Resources.GetString(Resource.String.syncfusion_licence));


            SetContentView(Resource.Layout.activity_main);

            // Initialize cheeseknife
            Cheeseknife.Inject(this);


            // ------- Save generate 10 saunas for demonstration purposes ------- //
            Helpers.Helpers.SaveSaunaGenerate(10);
            // ------- // ------- //


            // Set custom toolbar back icon
            toolbar.NavigationIcon = GetDrawable(Resource.Mipmap.ic_back);


            // Set actionbar
            SetSupportActionBar(toolbar);

            // Enable back press
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            //TODO: Here should be set the sauna ID for which we are viewing settings
            textViewSaunaNameSettings.Text = "Sauna 1";


            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame_layout_settings, SaunaSettingsFragment.NewInstance(1)).Commit();
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();

            return base.OnSupportNavigateUp();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        /// <summary>
        /// Method callback when user selects a lamp color. Returned to main activity from which is called, then send to requesting fragment
        /// 
        /// </summary>
        /// <param name="dialogId">Color Dialog ID</param>
        /// <param name="color">Selected color by user.</param>
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

