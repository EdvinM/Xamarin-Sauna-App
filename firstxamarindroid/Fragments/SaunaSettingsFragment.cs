
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
using Android.Flexbox;

namespace firstxamarindroid.SettingsModule
{
    public class SaunaSettingsFragment : Fragment
    {
        // Inject used views into the variables
        [InjectView(Resource.Id.recyclerViewSettings)]
        private RecyclerView recyclerViewSettings;

        // Declare other variables
        private SettingsListAdapter settingsListAdapter;

        private List<SettingItemModel> settingItemModelsList;

        private SaunaModel saunaModel;

        public static SaunaSettingsFragment NewInstance(int saunaId)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt(Helpers.Helpers.ARG_1, saunaId);

            SaunaSettingsFragment saunaSettingsFragment = new SaunaSettingsFragment();
            saunaSettingsFragment.Arguments = bundle;

            return saunaSettingsFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Get model serializable
            if (Arguments != null)
            {
                int saunaId = Arguments.GetInt(Helpers.Helpers.ARG_1);

                this.saunaModel = DbController.Instance.GetSauna(saunaId);


                // Generate fake menu items based on settings for each sauna
                this.settingItemModelsList = new List<SettingItemModel>()
                {
                    new SettingItemModel("Ventilation",
                                        (this.saunaModel.Ventilation.Status ? "On" : "Off"),
                                        Resource.Mipmap.ic_settings_ventilation,
                                        VentilationFragment.NewInstance(saunaId)),

                    new SettingItemModel("Heater",
                                        (this.saunaModel.Heater.Status ? "On" : "Off"),
                                        Resource.Mipmap.ic_settings_heating,
                                        HeaterFragment.NewInstance(saunaId)),

                    new SettingItemModel("Lights",
                                        (this.saunaModel.LightsStatus ? "On" : "Off"),
                                        Resource.Mipmap.ic_settings_lights,
                                        LightsFragment.NewInstance(saunaId)),

                    new SettingItemModel("Sound",
                                        (this.saunaModel.Sound.Status ? "On" : "Off"),
                                        Resource.Mipmap.ic_settings_sound,
                                        SoundFragment.NewInstance(saunaId)),
                };

                this.settingsListAdapter = new SettingsListAdapter(this.settingItemModelsList);
            }
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

            FlexboxLayoutManager flexboxLayoutManager = new FlexboxLayoutManager(this.Activity, FlexDirection.Row, FlexWrap.Wrap);
            flexboxLayoutManager.JustifyContent = JustifyContent.Center;
            flexboxLayoutManager.AlignItems = AlignItems.Stretch;
            flexboxLayoutManager.FlexWrap = FlexWrap.Wrap;

            this.recyclerViewSettings.SetLayoutManager(flexboxLayoutManager);

            this.settingsListAdapter.OnItemClick += SettingsListAdapter_OnItemClick;
        }


        private void SettingsListAdapter_OnItemClick(object sender, SettingItemModel e)
        {
            this.Activity.SupportFragmentManager.BeginTransaction().Replace(this.Id, e.GetFragment).AddToBackStack(e.GetFragment.Class.Name).Commit();
        }
    }
}
