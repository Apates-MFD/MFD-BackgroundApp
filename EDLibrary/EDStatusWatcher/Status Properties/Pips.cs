using System;
using System.Collections.Generic;

namespace EDLibrary.EDStatusWatcher
{
    public class Pips
    {
        public int SYS { get; set; }
        public int ENG { get; set; }
        public int WEP { get; set; }

        public Pips(List<int> pips)
        {
            if (pips != null)
            {
                SYS = pips[0];
                ENG = pips[1];
                WEP = pips[2];
            }
        }

        public override bool Equals(Object other)
        {
            if (other.GetType() != this.GetType()) return false;
            Pips otherPips = (Pips)other;
            return (this.SYS == otherPips.SYS &&
                    this.ENG == otherPips.ENG &&
                    this.WEP == otherPips.WEP);
        }

        public override string ToString()
        {
            return "{ SYS: " + this.SYS.ToString() + ", ENG: " + this.ENG.ToString() + " WEP: " + this.WEP.ToString() + " }";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SYS, ENG, WEP);
        }
    }
}
