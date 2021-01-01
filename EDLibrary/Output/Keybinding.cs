using EDLibrary.EDControllService.Menu;

namespace EDLibrary.Output
{
    /// <summary>
    /// Single Keybind
    /// <para>Link between <see cref="Actions"/> and a sequence of <see cref="DirectXKeyStrokes"/></para>
    /// </summary>
    public class Keybinding
    {
        public Actions Action { get; set; }
        public DirectXKeyStrokes[] KeyStrokes { get; set; }
    }
}
