using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace EDLibrary.EDStatusWatcher
{
    /// <summary>
    /// Parses Json file
    /// </summary>
    class StatusParser
    {
        public static void Parse()
        {
            using (FileStream fileStream = File.Open(Constants.PathToStatus, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BinaryReader reader = new BinaryReader(fileStream, Encoding.UTF8, true))
            {
                if (fileStream.Length == 0) return;
                var readOnlySpan = new ReadOnlySpan<byte>(reader.ReadBytes((int)fileStream.Length));
                if (readOnlySpan.Length == 0) return;
                try
                {
                    SerializeableStatus status = JsonSerializer.Deserialize<SerializeableStatus>(readOnlySpan);
                    Status.Instance.updateStatus(status);
                }
                catch (Exception)
                {
                    Debug.Assert(false);
                }
            }
        }

    }

}
