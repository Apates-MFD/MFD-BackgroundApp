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
        /// <para>Filepath: <seealso cref="Constants.PathToKeybindings"/></para>
        /// </summary>
        /// <returns></returns>
        public static List<Keybinding> Parse()
        {
            return JsonConvert.DeserializeObject<List<Keybinding>>(File.ReadAllText(Constants.PathToKeybindings));
        }
    }
}
