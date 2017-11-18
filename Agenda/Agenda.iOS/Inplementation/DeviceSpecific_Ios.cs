using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agenda.iOS.Inplementation;
using Foundation;
using UIKit;
using Agenda.iOS;
using System.Threading;
using Agenda.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceSpecific_Ios))]
namespace Agenda.iOS.Inplementation
{
    public class DeviceSpecific_Ios : IDeviceSpecific
    {
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}