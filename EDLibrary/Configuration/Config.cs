using EDLibrary.ControllInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDLibrary.Configuration
{
    class Config
    {
        public List<InputDeviceNames> InputDevices { get; set; }
        public string PathToMenuFolder { get; set; }
        public string MainMenuName { get; set; }
        public bool ButtonTriggerOnPress { get; set; }
        public string PathToStatusFolder { get; set; }
        public string PathToKeybindings { get; set; }
    }
}
