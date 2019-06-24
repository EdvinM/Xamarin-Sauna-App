
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
using firstxamarindroid.Models;

namespace firstxamarindroid.SettingsModule
{
    public class LightsFragment : Fragment
    {
        // Create a fictive list of available lamps
        private List<LightModel> lightModels = new List<LightModel>()
        {
            new LightModel("Lamp 1", false, 100, false, -1),
            new LightModel("Lamp 2", true, 100, false, -1),
        };

        // Bind views to variables
        [InjectView(Resource.Id.recyclerViewLights)]
        private RecyclerView recyclerViewLights;

        private LightsAdapter lightsAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.lights_fragment, container, false);

            Cheeseknife.Inject(this, view);

            this.lightsAdapter = new LightsAdapter(this.Activity, this.lightModels);

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
            
        }
    }
}
