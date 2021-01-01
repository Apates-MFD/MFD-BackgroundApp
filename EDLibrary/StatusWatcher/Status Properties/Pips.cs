using System;
using System.Collections.Generic;

namespace EDLibrary.StatusWatcher
{
    /// <summary>
    /// Power distribution of players ship in Elite Dangerous
    /// </summary>
    public class Pips
    {
        public int SYS { get; set; }
        public int ENG { get; set; }
        public int WEP { get; set; }

        /// <summary>
        /// Constructur 
        /// </summary>
        /// <param name="pips"></param>
        public Pips(List<int> pips)
        {
            if (pips != null)
            {
                SYS = pips[0];
                ENG = pips[1];
                WEP = pips[2];
            }
        }

        /// <summary>
        /// Overriden Equals method
        /// </summary>
        /// <param name="other"></param>
        /// <returns><see langword="false"/> if type or power values missmatches; Otherwise, <see langword="true"/></returns>
        public override bool Equals(Object other)
        {
            if (other.GetType() != this.GetType()) return false;
            Pips otherPips = (Pips)other;
            return (this.SYS == otherPips.SYS &&
                    this.ENG == otherPips.ENG &&
                    this.WEP == otherPips.WEP);
        }

        /// <summary>
        /// Overriden Hash method
        /// </summary>
        /// <returns>Hash of all 3 power values</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(SYS, ENG, WEP);
        }
    }
}
