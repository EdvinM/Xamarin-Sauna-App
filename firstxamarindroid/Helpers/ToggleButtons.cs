using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.Models;

namespace firstxamarindroid.Helpers
{
    public class ToggleButtons
    {
        [InjectView(Resource.Id.buttonSettingOn)]
        public Button buttonSettingOn;

        [InjectView(Resource.Id.buttonSettingOff)]
        public Button buttonSettingOff;

        private Context context;

        // Variable to hold sauna model in this view
        private SaunaModel saunaModel;

        // Event which will send toggle status back to the parent class
        public event EventHandler<bool> OnToggleChanged;



        public ToggleButtons(View view, SaunaModel saunaModel)
        {
            Cheeseknife.Inject(this, view);

            this.context    = view.Context;
            this.saunaModel = saunaModel;
        }



        /// <summary>
        /// Method called to update views based on active parameter
        /// 
        /// </summary>
        /// <param name="active">Specifies if view should be visible or not</param>
        public void UpdateToggleButtons(bool active)
        {
            buttonSettingOn.Background  = this.context.GetDrawable((active ? Resource.Drawable.button_on_pressed : Resource.Drawable.button_on_not_pressed));
            buttonSettingOff.Background = this.context.GetDrawable((active ? Resource.Drawable.button_off_not_pressed : Resource.Drawable.button_off_pressed));

            buttonSettingOn.SetTextColor(active ? new Color(Resource.Color.colorPrimaryGreen) : Color.White);
            buttonSettingOff.SetTextColor(!active ? new Color(Resource.Color.colorPrimaryRed) : Color.White);
        }



        /// <summary>
        /// Method to handle button click listeners for on/off buttons
        /// 
        /// </summary>
        /// <param name="sender">Button id</param>
        /// <param name="e">Click parameters</param>
        [InjectOnClick(Resource.Id.buttonSettingOn)]
        void ButtonSettingOn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Wait for the result from server
            if (SendDataToServer())
            {
                OnToggleChanged(this, button == buttonSettingOn);
                UpdateToggleButtons(button == buttonSettingOn);
            }
            else
            {
                //Display popup message
            }
        }


        [InjectOnClick(Resource.Id.buttonSettingOff)]
        void ButtonSettingOff_Click(object sender, EventArgs e)
        {
            ButtonSettingOn_Click(sender, e);
        }



        /// <summary>
        /// Method which is called in both button settings click listeners, to send data to server and update sauna's
        /// 
        /// </summary>
        private bool SendDataToServer()
        {
            // REFIT or another library to send data to server
            

            return true;
        }
    }
}
