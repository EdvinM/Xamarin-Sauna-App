using System;
using Android.Runtime;
using Java.IO;

namespace firstxamarindroid.Models
{
    [Serializable]
    public class LightModel : BaseModel
    {
        private String _name;

        private Boolean _status;
        private int _brightness;

        private Boolean _colorStatus;
        private int _color;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Boolean Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        public Boolean ColorStatus
        {
            get { return _colorStatus; }
            set { _colorStatus = value; }
        }

        public int Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public LightModel(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {

        }

        public LightModel(String _name, Boolean _status, int _brightness, Boolean _colorStatus, int _color)
        {
            this._name          = _name;
            this._status        = _status;
            this._brightness    = _brightness;
            this._colorStatus   = _colorStatus;
            this._color         = _color;
        }
    }
}
