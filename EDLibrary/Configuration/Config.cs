using EDLibrary.ControllInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDLibrary.Configuration
{
    class Config
    {
        [NonSerialized] public List<InputDeviceNames> InputDevices;
        public List<string> InputDevicesStrings { get; set; }       
        public string PathToMenuFolder { get; set; }
        public string MainMenuName { get; set; }
        public bool ButtonTriggerOnPress { get; set; }
        public string PathToStatusFolder { get; set; }
        public string PathToConfiguration { get; set; }
        public Dictionary<string,double[]> WindowProperties { get; set; }
        public Dictionary<string, Display.DisplaySettings> DisplaySettings { get; set; }
        public bool ReadStatus { get; set; } = true;
        public bool WriteOutput { get; set; } = true;
    }
}
