using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using firstxamarindroid.Models;

namespace firstxamarindroid.Helpers
{
    public class SettingsListAdapter : RecyclerView.Adapter
    {
        private List<SettingItemModel> settingItemModelsList;

        public SettingsListAdapter(List<SettingItemModel> SettingItemModelsList)
        {
            this.settingItemModelsList = SettingItemModelsList;
        }

        public override int ItemCount => this.settingItemModelsList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SettingItemModel settingItemModel = this.settingItemModelsList[position];


            SettingsItemViewHolder settingsItemViewHolder = (SettingsItemViewHolder)holder;
            settingsItemViewHolder.textViewSettingName.Text = settingItemModel.Name;
            settingsItemViewHolder.textViewSettingStatus.Text = settingItemModel.Status;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.setting_item_layout, parent, false);

            return new SettingsItemViewHolder(view);
        }
    }
}
