
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V7.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.Helpers;
using firstxamarindroid.Models;
using Android.Support.V4.App;

namespace firstxamarindroid.SettingsModule
{
    public class SaunaSettingsFragment : Fragment
    {
        // Inject used views into the variables
        [InjectView(Resource.Id.recyclerViewSettings)]
        private RecyclerView recyclerViewSettings;

        // Declare other variables
        private SettingsListAdapter settingsListAdapter;

        private List<SettingItemModel> settingItemModelsList = new List<SettingItemModel>()
        {
            new SettingItemModel("Ventilation controlling system", "Off", new VentilationFragment()),
            new SettingItemModel("Heater controlling system", "Off", new HeaterFragment()),
            new SettingItemModel("Lights controlling system", "Off", new LightsFragment()),
            new SettingItemModel("Sound controlling system", "Off", new SoundFragment()),
        };


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.settingsListAdapter = new SettingsListAdapter(this.settingItemModelsList);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.sauna_settings_fragment, container, false);

            // Initialize cheeseknife view injection.
            Cheeseknife.Inject(this, view);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            this.recyclerViewSettings.SetAdapter(this.settingsListAdapter);
            this.recyclerViewSettings.SetLayoutManager(new LinearLayoutManager(view.Context));

            this.settingsListAdapter.OnItemClick += SettingsListAdapter_OnItemClick;
        }


        private void SettingsListAdapter_OnItemClick(object sender, SettingItemModel e)
        {
            this.Activity.SupportFragmentManager.BeginTransaction().Replace(this.Id, e.GetFragment).AddToBackStack(e.GetFragment.Class.Name).Commit();
        }
    }
}
