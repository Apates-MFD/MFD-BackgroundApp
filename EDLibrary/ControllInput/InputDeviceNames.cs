using System;

namespace EDLibrary.ControllInput
{
    /// <summary>
    /// This is a enum with strings workaround
    /// </summary>
    
    public class InputDeviceNames
    {
        private InputDeviceNames(string value) { Value = value; }
        public string Value { get; set; }

        #region Adjust
        /// <summary>
        /// These are the Enum Members.
        /// The sting is the display name from the device.
        /// Adjust to device Name
        /// </summary>
        public static InputDeviceNames MFD_ONE { get { return new InputDeviceNames("F16 MFD 1"); } }
        public static InputDeviceNames MFD_TWO { get { return new InputDeviceNames("F16 MFD 2"); } }
        public static InputDeviceNames MFD_THREE { get { return new InputDeviceNames("F16 MFD 3"); } }
        public static InputDeviceNames MFD_FOUR { get { return new InputDeviceNames("F16 MFD 4"); } }
        public static InputDeviceNames MFD_FIVE { get { return new InputDeviceNames("F16 MFD 5"); } }
        public static InputDeviceNames MFD_SIX { get { return new InputDeviceNames("F16 MFD 6"); } }
        public static InputDeviceNames MFD_SEVEN { get { return new InputDeviceNames("F16 MFD 7"); } }
        public static InputDeviceNames MFD_EIGHT { get { return new InputDeviceNames("F16 MFD 8"); } }

        /// <summary>
        /// Fixed array with all defined Names
        /// </summary>
        /// <returns><see cref="InputDeviceNames[]"/></returns>
        public static InputDeviceNames[] GetAll()
        {
            return new[] { MFD_ONE, MFD_TWO, MFD_THREE, MFD_FOUR, MFD_FIVE, MFD_SIX, MFD_SEVEN, MFD_EIGHT };
        }
        #endregion

        /// <summary>
        /// Ovverriden Equals Method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// <see langword="false"/> if object type or Value missmatches;otherwise, <see langword="true"/>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            InputDeviceNames other = (InputDeviceNames)obj;
            return this.Value.Equals(other.Value);
        }

        /// <summary>
        /// Overriden Hash method
        /// </summary>
        /// <returns>Hash of value</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }

}
