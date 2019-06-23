using System;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Lilarcor.Cheeseknife;

namespace firstxamarindroid.Helpers
{
    public class SettingsItemViewHolder : RecyclerView.ViewHolder
    {
        public SettingsItemViewHolder(View view) : base(view)
        {
            Cheeseknife.Inject(this, view);
        }
    }
}
