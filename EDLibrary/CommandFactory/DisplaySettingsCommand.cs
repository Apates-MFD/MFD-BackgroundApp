using System;
using System.Collections.Generic;
using System.Text;

namespace EDLibrary.CommandFactory
{
    class DisplaySettingsCommand : Command
    {
        public string Type { get; set; }
        public string SubType { get; set; }
        
        public object Device { get; set; }
        public override void Execute(object sender)
        {
            if (Type == null || SubType == null) throw new ArgumentException("Sub/Type cannot be null");
            if(Device == null) throw new ArgumentException("Device cannot be null");

            switch (Type)
            {
                case "LOAD":
                    ((Controller)sender).SetDisplaySettings(Device, SubType);
                    break;
                case "SAVE":
                    ((Controller)sender).SaveDisplaySettings(Device, SubType);
                    break;

                case "RESET":
                    int t = 0;
                    switch (SubType)
                    {
                        case "BRT":
                            break;

                        case "CON":
                            t = 1;
                            break;

                        case "SYM":
                            t = 2;
                            break;
                        
                        default:
                            throw new ArgumentException("Unknonw subtype");
                    }
                    ((Controller)sender).ResetDisplaySetting(Device, t);
                    break;
                default:
                    throw new ArgumentException("Unknonw type");
            }
            
        }
    }
}
