using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;

namespace firstxamarindroid.Helpers
{
    public class SettingsItemViewHolder : RecyclerView.ViewHolder
    {
        [InjectView(Resource.Id.settingItemLayout)]
        public LinearLayout settingItemLayout;

        [InjectView(Resource.Id.textViewSettingName)]
        public TextView textViewSettingName;

        [InjectView(Resource.Id.textViewSettingStatus)]
        public TextView textViewSettingStatus;

        [InjectView(Resource.Id.imageViewSettingIcon)]
        public ImageView imageViewSettingIcon;

        public SettingsItemViewHolder(View view) : base(view)
        {
            Cheeseknife.Inject(this, view);
        }
    }
}
