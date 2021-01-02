using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace EDLibrary.Output
{
    /// <summary>
    /// Keybinding parser
    /// </summary>
    public class KeybindingParser
    {
        /// <summary>
        /// Parses Keybindings from config file.
        /// </summary>
        /// <returns></returns>
        public static List<Keybinding> Parse(string pathToKeybindingConfig)
        {
            return JsonConvert.DeserializeObject<List<Keybinding>>(File.ReadAllText(pathToKeybindingConfig));
        }
    }
}
