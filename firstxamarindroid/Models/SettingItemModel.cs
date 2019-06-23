using System;
using Android.Support.V4.App;

namespace firstxamarindroid.Models
{
    public class SettingItemModel
    {
        private String _Name;
        private String _Status;
        private Fragment _Fragment;

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public String Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Fragment GetFragment
        {
            get { return _Fragment; }
            set { _Fragment = value; }
        }

        public SettingItemModel()
        {

        }

        public SettingItemModel(String Name, String Status, Fragment fragment)
        {
            this.Name       = Name;
            this.Status     = Status;
            this._Fragment  = fragment;
        }
    }
}
