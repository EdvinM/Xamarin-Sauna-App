using System;
using System.Linq;
using firstxamarindroid.Models;
using Realms;

namespace firstxamarindroid.Helpers
{
    public sealed class DbController
    {
        private static DbController instance = null;
        private static readonly object padlock = new object();

        public static DbController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DbController();
                    }
                    return instance;
                }
            }
        }

        public DbController()
        {
            // Basic realm configuration settings. Specified to delete saved models when something was added to the model schema.
            RealmConfiguration realmConfiguration = new RealmConfiguration();
            realmConfiguration.ShouldDeleteIfMigrationNeeded = true;
            realmConfiguration.SchemaVersion = 1;
        }


        /// <summary>
        /// Method which queries for Sauna with specific id and returns the result.
        /// 
        /// </summary>
        /// <param name="id">Sauna ID</param>
        /// <returns>Found sauna model or NULL otherwise</returns>
        public SaunaModel GetSauna(int id)
        {
            var saunas = Realm.GetInstance().All<SaunaModel>().Where(p => p.Id == id);

            return (saunas != null && saunas.Any<SaunaModel>()) ? saunas.First<SaunaModel>() : null;
        }


        /// <summary>
        /// Method which adds new sauna to database
        /// 
        /// </summary>
        /// <param name="saunaModel">SaunaModel with all parameters set</param>
        public void SetSauna(SaunaModel saunaModel)
        {
            var realm = Realm.GetInstance();
            realm.Write(() => realm.Add(saunaModel));
        }


        /// <summary>
        /// Method which returns a specific light model based on given parameters
        /// 
        /// </summary>
        /// <param name="saunaId">Sauna ID</param>
        /// <param name="lightId">Light ID</param>
        /// <returns></returns>
        public LightModel GetLight(int saunaId, String lightId)
        {
            var sauna = this.GetSauna(saunaId);

            return sauna.Lights.First<LightModel>(l => l.Id == lightId);
        }
    }
}
