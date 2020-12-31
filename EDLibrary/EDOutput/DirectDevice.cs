using System;
using System.Collections.Generic;
using System.Text;
namespace EDLibrary.EDOutput
{
    public class DirectDevice
    {
        public DirectDevice()
        {
            //@"\\?\hid#vid_044f&pid_b352#7&15f387d&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}"
            USBHIDDRIVER.USBInterface usb = new USBHIDDRIVER.USBInterface("044f");

  
        }
    }
}
