using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
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


            SettingsItemViewHolder settingsItemViewHolder = (SettingsItemViewHolder)holder;
            settingsItemViewHolder.textViewSettingName.Text = settingItemModel.Name;
            settingsItemViewHolder.textViewSettingStatus.Text = settingItemModel.Status;

            // Add layout setting position tag, so we can access it from click handler function
            settingsItemViewHolder.settingItemLayout.SetTag(Resource.String.res_setting_tag_id, position);

            // Add click listener to setting main layout
            settingsItemViewHolder.settingItemLayout.Click += SettingItemLayout_Click;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.setting_item_layout, parent, false);

            return new SettingsItemViewHolder(view);
        }

        /// <summary>
        ///  Method to handle click listener on main item layout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingItemLayout_Click(object sender, EventArgs e)
        {
            // Get the position from parent layout tag
            int pos = (int)((LinearLayout)sender).Tag;

            this.OnItemClick(this, this.settingItemModelsList[pos]);
        }
    }
}
