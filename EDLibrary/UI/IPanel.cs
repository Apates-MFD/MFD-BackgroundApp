namespace EDLibrary.UI
{
    /// <summary>
    /// Interface of WPF Window
    /// </summary>
    public interface IPanel
    {
        /// <summary>
        /// Sets text for a given position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="text"></param>
        public void SetText(int position, string text);

        /// <summary>
        /// resets all text to empty
        /// </summary>
        public void Clear();

        /// <summary>
        /// sets invert state from textblock
        /// </summary>
        /// <param name="position"></param>
        /// <param name="inverted"></param>
        public void SetInverted(int position, bool inverted);

        /// <summary>
        /// Sets enabled
        /// </summary>
        /// <param name="position"></param>
        /// <param name="enabled"></param>
        public void SetEnabled(int position, bool enabled);
        /// <summary>
        /// Returns current window postion
        /// </summary>
        /// <returns></returns>
        public double[] GetWindowProperty();

        /// <summary>
        /// Sets window position
        /// </summary>
        /// <param name="porperty"></param>
        public void SetWindowProperty(double[] porperty);

        public void IncreaseBrightness();
        public void DecreaseBrightness();

        public void IncreaseContrast();
        public void DecreaseContrast();
    }
}
