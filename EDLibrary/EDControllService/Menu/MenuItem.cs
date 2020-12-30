using EDLibrary.EDControllService.CommandFactory;

namespace EDLibrary.EDControllService.Menu
{
    public class MenuItem
    {
        public string DisplayText { get; set; }
        public bool State { get; set; }
        public int Position { get; set; }
        public string LinkedProperty { get; set; }
        public SerializableCommand Command { get; set; }
    }

}
