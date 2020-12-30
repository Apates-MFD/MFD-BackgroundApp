using System;

namespace EDLibrary.EDStatusInput
{
    public class MFDType
    {
        private MFDType(string value) { Value = value; }
        public string Value { get; set; }

        public static MFDType MFD_ONE { get { return new MFDType("F16 MFD 1"); } }
        public static MFDType MFD_TWO { get { return new MFDType("F16 MFD 2"); } }
        public static MFDType MFD_THREE { get { return new MFDType("F16 MFD 3"); } }
        public static MFDType MFD_FOUR { get { return new MFDType("F16 MFD 4"); } }
        public static MFDType MFD_FIVE { get { return new MFDType("F16 MFD 5"); } }
        public static MFDType MFD_SIX { get { return new MFDType("F16 MFD 6"); } }
        public static MFDType MFD_SEVEN { get { return new MFDType("F16 MFD 7"); } }
        public static MFDType MFD_EIGHT { get { return new MFDType("F16 MFD 8"); } }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            MFDType other = (MFDType)obj;
            return this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }

}
