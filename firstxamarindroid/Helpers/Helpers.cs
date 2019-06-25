using System;
using Android.Graphics;
using Android.Util;
using firstxamarindroid.Models;

namespace firstxamarindroid.Helpers
{
    public class Helpers
    {
        public Helpers()
        {
        }


        // Bundle key constants for fragments.
        public static String ARG_1 = "ARG_1";
        public static String ARG_2 = "ARG_2";
        public static String ARG_3 = "ARG_3";
        public static String ARG_4 = "ARG_4";


        public static void SaveSaunaGenerate(int count)
        {
            // Check if we have records in database, if not generate saunas
            if(DbController.Instance.GetSauna(1) == null)
            {
                Log.Debug("Helpers", "No saunas found in database, started generating " + count + " saunas");

                GenerateDummySaunas(count);
            }
        }

        public static void GenerateDummySaunas(int count)
        {
            for(int i = 1; i <= count; i++)
            {
                SaunaModel saunaModel = new SaunaModel();
                saunaModel.Id   = i;
                saunaModel.Name = "Sauna " + i;

                // Add heater setting
                saunaModel.Heater = new HeaterModel(i, false, "LOW", 50);

                // Add 2 lights
                for(int j = 1; j <= 2; j++)
                {
                    saunaModel.Lights.Add(new LightModel(i + j, "Lamp " + j, false, 1, false, Color.White));
                }

                // Add sound setting
                saunaModel.Sound = new SoundModel(i, 0);

                // Add ventilation
                saunaModel.Ventilation = new VentilationModel(i, false, false, false, 100, 100);


                // Add & save sauna to our local database
                DbController.Instance.SetSauna(saunaModel);
            }
        }
    }
}
