
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
        /*
         ************************************
         *      Bind Views to Variables
         ************************************
         */

        [InjectView(Resource.Id.recyclerViewSettings)]
        private RecyclerView recyclerViewSettings;


        /*
         ************************************
         *      Declare Variables
         ************************************
         */
        private SettingsListAdapter settingsListAdapter;

        private List<SettingItemModel> settingItemModelsList;

        private SaunaModel saunaModel;


        /*
        ************************************
        *      Fragment methods
        ************************************
        */

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
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.sauna_settings_fragment, container, false);

            // Initialize cheeseknife view injection.
            Cheeseknife.Inject(this, view);

            // Function to regenerate settings menu items on each view creation.
            GenerateSettingsMenuItems();
            this.settingsListAdapter = new SettingsListAdapter(this.settingItemModelsList);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            this.recyclerViewSettings.SetAdapter(this.settingsListAdapter);

            // Create flexbox layout manager to fit all sizes of screens.
            FlexboxLayoutManager flexboxLayoutManager   = new FlexboxLayoutManager(this.Activity, FlexDirection.Row, FlexWrap.Wrap);
            flexboxLayoutManager.JustifyContent         = JustifyContent.Center;
            flexboxLayoutManager.AlignItems             = AlignItems.Stretch;
            flexboxLayoutManager.FlexWrap               = FlexWrap.Wrap;

            this.recyclerViewSettings.SetLayoutManager(flexboxLayoutManager);

            this.settingsListAdapter.OnItemClick += SettingsListAdapter_OnItemClick;
        }


        /*
         ************************************
         *      Callbacks
         ************************************
         */

        private void SettingsListAdapter_OnItemClick(object sender, SettingItemModel e)
        {
            this.Activity.SupportFragmentManager.BeginTransaction().Replace(this.Id, e.GetFragment).AddToBackStack(e.GetFragment.Class.Name).Commit();
        }



        private void GenerateSettingsMenuItems()
        {
            // Generate fake menu items based on settings for each sauna
            this.settingItemModelsList = new List<SettingItemModel>()
            {
                new SettingItemModel("Ventilation",
                                    (this.saunaModel.Ventilation.Status ? "On" : "Off"),
                                    Resource.Mipmap.ic_settings_ventilation,
                                    VentilationFragment.NewInstance(this.saunaModel.Id)),

                new SettingItemModel("Heater",
                                    (this.saunaModel.Heater.Status ? "On" : "Off"),
                                    Resource.Mipmap.ic_settings_heating,
                                    HeaterFragment.NewInstance(this.saunaModel.Id)),

                new SettingItemModel("Lights",
                                    (this.saunaModel.LightsStatus ? "On" : "Off"),
                                    Resource.Mipmap.ic_settings_lights,
                                    LightsFragment.NewInstance(this.saunaModel.Id)),

                new SettingItemModel("Sound",
                                    (this.saunaModel.Sound.Status ? "On" : "Off"),
                                    Resource.Mipmap.ic_settings_sound,
                                    SoundFragment.NewInstance(this.saunaModel.Id)),
            };
        }
    }
}
