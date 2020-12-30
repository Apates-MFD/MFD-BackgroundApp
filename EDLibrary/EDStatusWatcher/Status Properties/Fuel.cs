using System;

namespace EDLibrary.EDStatusWatcher
{
    [Serializable]
    public class Fuel
    {
        public double FuelMain { get; set; }
        public double FuelReservoir { get; set; }

        public override bool Equals(Object other)
        {
            if (other.GetType() != this.GetType()) return false;
            Fuel ohterFuel = (Fuel)other;
            return (ohterFuel.FuelMain == this.FuelMain && ohterFuel.FuelReservoir == this.FuelReservoir);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FuelMain, FuelReservoir);
        }

        public override string ToString()
        {
            return "Fuel: {Main: " + this.FuelMain.ToString() + " Reservoir: " + this.FuelReservoir.ToString() + " }";
        }
    }
}
