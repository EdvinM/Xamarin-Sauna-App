using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;

namespace firstxamarindroid.Helpers
{
    public class ToggleButtons
    {
        [InjectView(Resource.Id.buttonSettingOn)]
        public Button buttonSettingOn;

        [InjectView(Resource.Id.buttonSettingOff)]
        public Button buttonSettingOff;

        private Context context;

        private bool Active;

        // Event which will send toggle status back to the parent class
        public event EventHandler<bool> OnToggleChanged;

        public ToggleButtons(View view)
        {
            Cheeseknife.Inject(this, view);

            this.context = view.Context;
        }

        public void UpdateToggleButtons(bool active)
        {
            this.Active = active;

            buttonSettingOn.Background  = this.context.GetDrawable((this.Active ? Resource.Drawable.button_on_pressed : Resource.Drawable.button_on_not_pressed));
            buttonSettingOff.Background = this.context.GetDrawable((this.Active ? Resource.Drawable.button_off_not_pressed : Resource.Drawable.button_off_pressed));

            buttonSettingOn.SetTextColor(this.Active ? new Color(Resource.Color.colorPrimaryGreen) : Color.White);
            buttonSettingOff.SetTextColor(!this.Active ? new Color(Resource.Color.colorPrimaryRed) : Color.White);
        }

        [InjectOnClick(Resource.Id.buttonSettingOn)]
        void ButtonSettingOn_Click(object sender, EventArgs e)
        {
            OnToggleChanged(this, true);
            UpdateToggleButtons(true);
        }

        [InjectOnClick(Resource.Id.buttonSettingOff)]
        void ButtonSettingOff_Click(object sender, EventArgs e)
        {
            OnToggleChanged(this, false);
            UpdateToggleButtons(false);
        }
    }
}
