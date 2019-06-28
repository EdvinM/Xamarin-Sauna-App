using System;
using Android.Support.V4.App;

namespace firstxamarindroid.Models
{
    public class SettingItemModel
    {
        private String _Name;
        private String _Status;
        private int _Icon;
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
        public int Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
        public Fragment GetFragment
        {
            get { return _Fragment; }
            set { _Fragment = value; }
        }

        public SettingItemModel()
        {

        }

        public SettingItemModel(String Name, String Status, int Icon, Fragment fragment)
        {
            this.Name       = Name;
            this.Status     = Status;
            this.Icon       = Icon;
            this._Fragment  = fragment;
        }
    }
}
