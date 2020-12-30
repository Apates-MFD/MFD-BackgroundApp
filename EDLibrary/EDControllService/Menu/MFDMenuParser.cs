using Newtonsoft.Json;
using System.IO;

namespace EDLibrary.EDControllService.Menu
{
    public static class MFDMenuParser
    {
        public static void Serialize(MFDMenu menu, string path)
        {
            File.Delete(path);
            File.WriteAllText(path,JsonConvert.SerializeObject(menu, Formatting.Indented));
        }

        public static MFDMenu Deserialize(string path)
        {
            return JsonConvert.DeserializeObject<MFDMenu>(File.ReadAllText(path));
        }
    }
}
