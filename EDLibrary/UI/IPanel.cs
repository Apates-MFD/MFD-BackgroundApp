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

        /// <summary>
        /// Increases Brightness
        /// </summary>
        public void IncreaseBrightness();

        /// <summary>
        /// Decreases Brightness
        /// </summary>
        public void DecreaseBrightness();

        /// <summary>
        /// Increases Contrast (Sort of)
        /// </summary>
        public void IncreaseContrast();

        /// <summary>
        /// Decreases Contrast
        /// </summary>
        public void DecreaseContrast();

        /// <summary>
        /// Gets the current brt, cond & sym value
        /// </summary>
        /// <returns></returns>
        public double[] GetDisplaySetting();

        /// <summary>
        /// Sets the current brt, con & sym value
        /// </summary>
        public void SetDisplaySetting(double[] settings);
    }
}
