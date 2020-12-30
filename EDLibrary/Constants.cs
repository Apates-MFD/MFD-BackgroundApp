using System;

namespace EDLibrary
{
    /// <summary>
    /// Common Constants in EDStatusWatcher
    /// </summary>
    static class Constants
    {
        public static readonly string PathToStatusFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Saved Games\Frontier Developments\Elite Dangerous\";
        public static readonly string PathToStatus = PathToStatusFolder + "Status.json";
        public static readonly string PathToKeybindings = "config/keybindings.json";
    }
}
