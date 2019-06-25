using System;
using Android.Runtime;
using Java.Interop;
using Java.IO;

namespace firstxamarindroid.Models
{
    public class BaseModel : Java.Lang.Object, ISerializable
    {
        public BaseModel()
        {
        }

        public BaseModel(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        [Export("readObject", Throws = new[]
                                        {
                                        typeof(Java.IO.IOException),
                                        typeof(Java.Lang.ClassNotFoundException)
                                    }
        )]
        private void ReadObjectDummy(Java.IO.ObjectInputStream source)
        {
            
        }

        [Export("writeObject", Throws = new[]
        {
        typeof(Java.IO.IOException),
        typeof(Java.Lang.ClassNotFoundException)
    }
        )]
        private void WriteObjectDummy(Java.IO.ObjectOutputStream destination)
        {
            
        }
    }
}
