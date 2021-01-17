using System.Collections.Generic;

namespace NetworkLibrary.NetworkPackage.Commands
{
    public static class CommandInformations
    {
        public static readonly Dictionary<Commands_Button, string[]> ButtonParameterCount = new Dictionary<Commands_Button, string[]>()
        {
            {Commands_Button.SET_TEXT, new string[]{"Position","Text", "isInverted" }},
            {Commands_Button.CLEAR,new string[]{ } },
            {Commands_Button.SET_DISPLAY_SETTINGS,new string[]{"UNDEFINED", "UNDEFINED", "UNDEFINED" } }
        };
    }
}
