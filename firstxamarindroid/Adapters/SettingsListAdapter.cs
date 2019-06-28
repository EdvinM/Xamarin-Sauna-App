using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using firstxamarindroid.Models;

namespace firstxamarindroid.Helpers
{
    public class SettingsListAdapter : RecyclerView.Adapter
    {
        // Variable which holds the main settings list;
        private List<SettingItemModel> settingItemModelsList;

        // Declare the interface which we will use to send data to parent class when user clicked an item.
        public event EventHandler<SettingItemModel> OnItemClick;


        public SettingsListAdapter(List<SettingItemModel> SettingItemModelsList)
        {
            this.settingItemModelsList = SettingItemModelsList;
        }

        public override int ItemCount => this.settingItemModelsList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SettingItemModel settingItemModel = this.settingItemModelsList[position];

            // Set values corresponding to settings items.
            SettingsItemViewHolder settingsItemViewHolder = (SettingsItemViewHolder)holder;
            settingsItemViewHolder.textViewSettingName.Text = settingItemModel.Name;
            settingsItemViewHolder.textViewSettingStatus.Text = settingItemModel.Status;
            Glide.With(settingsItemViewHolder.ItemView.Context).Load(settingItemModel.Icon)
                .Apply(new Com.Bumptech.Glide.Request.RequestOptions().Override(64, 64))
                .Into(settingsItemViewHolder.imageViewSettingIcon);

            // Add layout setting position tag, so we can access it from click handler function
            settingsItemViewHolder.settingItemLayout.SetTag(Resource.String.res_setting_tag_id, position);

            // Add click listener to setting main layout
            settingsItemViewHolder.settingItemLayout.Click += (sender, e) => this.OnItemClick(this, this.settingItemModelsList[position]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.setting_item_layout, parent, false);

            return new SettingsItemViewHolder(view);
        }
    }
}
