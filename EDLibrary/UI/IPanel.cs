namespace EDLibrary.UI
{
    /// <summary>
    /// Interface of WPF Window
    /// </summary>
    public interface IPanel
    {
        public void SetText(int position, string text);
        public void Clear();
        public void SetInverted(int position, bool inverted);
        public void SetEnabled(int position, bool enabled);
    }
}
