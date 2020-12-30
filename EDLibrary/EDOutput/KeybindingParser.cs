using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace EDLibrary.EDOutput
{
    public class KeybindingParser
    {
        public static List<Keybinding> Parse()
        {
            return JsonConvert.DeserializeObject<List<Keybinding>>(File.ReadAllText(Constants.PathToKeybindings));
        }
    }
}
