
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace firstxamarindroid.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/MySplashTheme.Splash", Icon = "@mipmap/ic_logo", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug("SplashActivity", "Loading data...");
            await Task.Delay(500);

            Log.Debug("SplashActivity", "Data loading finished - starting MainActivity.");

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
