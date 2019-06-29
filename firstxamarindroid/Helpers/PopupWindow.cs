using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Syncfusion.Android.PopupLayout;

namespace firstxamarindroid.Helpers
{
    public class PopupWindow
    {
        // Dialog Layout
        private SfPopupLayout popupLayout;
        private TextView popupContent;

        private View hostingView;

        // Dialog properties
        public String Title { get; set; }
        public String Content { get; set; }
        public int Height { get; }

        
        public PopupWindow(){ }

        public PopupWindow(View view) : this(view, "", "", 250) { }

        public PopupWindow(View view, string title, string content, int height = 250)
        {
            this.hostingView = view;

            this.Title      = title;
            this.Content    = content;
            this.Height     = height;

            CreateLayout(view.Context);
        }

        private void CreateLayout(Context context)
        {
            // Create popup content
            popupContent = new TextView(context) { Text = this.Content };
            popupContent.TextAlignment = TextAlignment.ViewStart;
            popupContent.SetTextColor(Color.Black);
            popupContent.SetPadding(30, 0, 0, 0);

            // Create popup layout
            popupLayout = new SfPopupLayout(context);
            popupLayout.PopupView.HeaderTitle   = this.Title;
            popupLayout.PopupView.ContentView   = popupContent;
            popupLayout.PopupView.HeightRequest = this.Height;
            popupLayout.PopupView.ShowFooter    = false;

            popupLayout.Closed += PopupLayout_Closed;
        }

        private void PopupLayout_Closed(object sender, System.EventArgs e)
        {
            
        }

        public void Show()
        {
            this.Show(this.Title, this.Content);
        }

        public void Show(string title, String content)
        {   
            this.Title      = title;
            this.Content    = content;

            popupContent.Text                       = this.Content;

            popupLayout.PopupView.ContentView       = popupContent;
            popupLayout.PopupView.HeaderTitle       = this.Title;
            popupLayout.PopupView.ShowCloseButton   = true;
            popupLayout.IsOpen                      = true;
        }

        public void Hide()
        {
            popupLayout.IsOpen = false;
        }
    }
}
