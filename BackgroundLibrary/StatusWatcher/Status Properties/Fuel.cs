using System;

namespace BackgroundLibrary.StatusWatcher
{
    /// <summary>
    /// Fuel Class for Elite Dangerous
    /// </summary>
    [Serializable]
    public class Fuel
    {
        public double FuelMain { get; set; }
        public double FuelReservoir { get; set; }

        /// <summary>
        /// Overriden Equals method
        /// </summary>
        /// <param name="other"></param>
        /// <returns><see langword="false"/> if tpye or fuel values missmatches; Otherwise, <see langword="true"/></returns>
        public override bool Equals(Object other)
        {
            if (other.GetType() != this.GetType()) return false;
            Fuel ohterFuel = (Fuel)other;
            return (ohterFuel.FuelMain == this.FuelMain && ohterFuel.FuelReservoir == this.FuelReservoir);
        }

        /// <summary>
        /// Overriden Hash method
        /// </summary>
        /// <returns>Hash of <see cref="FuelMain"/> and <see cref="FuelReservoir"/></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FuelMain, FuelReservoir);
        }
    }
}
