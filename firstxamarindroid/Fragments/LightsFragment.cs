
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.Adapters;
using firstxamarindroid.Fragments;
using firstxamarindroid.Helpers;
using firstxamarindroid.Models;

namespace firstxamarindroid.SettingsModule
{
    public class LightsFragment : Fragment
    {
        // Bind views to variables
        [InjectView(Resource.Id.recyclerViewLights)]
        private RecyclerView recyclerViewLights;

        private LightsAdapter lightsAdapter;

        private SaunaModel saunaModel;


        public static LightsFragment NewInstance(int saunaId)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt(Helpers.Helpers.ARG_1, saunaId);

            LightsFragment lightsFragment = new LightsFragment();
            lightsFragment.Arguments = bundle;

            return lightsFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                int saunaId = Arguments.GetInt(Helpers.Helpers.ARG_1);

                this.saunaModel = DbController.Instance.GetSauna(saunaId);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.lights_fragment, container, false);

            Cheeseknife.Inject(this, view);

            this.lightsAdapter = new LightsAdapter(this.Activity, new List<LightModel>(this.saunaModel.Lights));

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            this.recyclerViewLights.SetAdapter(this.lightsAdapter);
            this.recyclerViewLights.SetLayoutManager(new LinearLayoutManager(view.Context));

            // Handle adapter item click listener
            this.lightsAdapter.OnItemClick += LightsAdapter_OnItemClick;
        }

        /// <summary>
        /// Click listener method, to handle item clicks on lamps. Navigate to lamp settings fragment when clicked on a given lamp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightsAdapter_OnItemClick(object sender, LightModel e)
        {
            this.Activity.SupportFragmentManager
                .BeginTransaction()
                .Replace(this.Id, LightSettingsFragment.NewInstance(this.saunaModel.Id, e.Id))
                .AddToBackStack("LightSettingsFragmentBackstack")
                .Commit();
        }
    }
}
