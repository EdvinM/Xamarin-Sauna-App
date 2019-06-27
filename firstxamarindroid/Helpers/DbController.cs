﻿using System;
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

        public SaunaModel GetSauna(int id)
        {
            var saunas = Realm.GetInstance().All<SaunaModel>().Where(p => p.Id == id);

            return (saunas != null && saunas.Any<SaunaModel>()) ? saunas.First<SaunaModel>() : null;
        }

        public void SetSauna(SaunaModel saunaModel)
        {
            var realm = Realm.GetInstance();
            realm.Write(() => realm.Add(saunaModel));
        }

        public LightModel GetLight(int saunaId, String lightId)
        {
            var sauna = this.GetSauna(saunaId);

            return sauna.Lights.First<LightModel>(l => l.Id == lightId);
        }
    }
}