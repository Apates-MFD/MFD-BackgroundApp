using System;
using System.ComponentModel;

namespace EDLibrary.EDStatusWatcher
{
    /// <summary>
    /// Singelton Status Instance with up to date information
    /// </summary>
    

    public class Status : INotifyPropertyChanged
    {

        public Pips Pips { get; set; } // Power Distribution
        public LegalState LegalState { get; set; } //Player state
        public int FireGroup { get; set; } //Active Firegroup
        public GuiFocus GuiFocus { get; set; }
        public Fuel Fuel { get; set; } //Fuel in tons
        public double Cargo { get; set; } //Cargo in tons
        public double Latitude { get; set; }//Positioning
        public double Longitude { get; set; }//Positioning
        public int Heading { get; set; }//Positioning
        public int Altitude { get; set; }//If on planet
        public string BodyName { get; set; }//Planet Name
        public string PlanetRadius { get; set; }//Radius of planet (Guess)

        //All Flags
        public bool DOCKED_LANDINGPAD { get; set; }
        public bool LANDED_SURFACE { get; set; }
        public bool LANDING_GEAR_DOWN { get; set; }
        public bool SHIELDS_UP { get; set; }
        public bool SUPERCRUISE { get; set; }
        public bool FLIGHTASSIST_OFF { get; set; }
        public bool HARDPOINTS_DEPLOYED { get; set; }
        public bool IN_WING { get; set; }
        public bool LIGHT_ON { get; set; }
        public bool CARGO_SCOOP_DEPLOYED { get; set; }
        public bool SILENT_RUNNING { get; set; }
        public bool SCOOPING_FUEL { get; set; }
        public bool SRV_HANDBREAK { get; set; }
        public bool SRV_USING_TURRET { get; set; }
        public bool SRV_TURRET_RETRACTED { get; set; }
        public bool SRV_DRIVEASSIS { get; set; }
        public bool FSD_MASSLOCKED { get; set; }
        public bool PSD_CHARGING { get; set; }
        public bool FSD_COOLDOWN { get; set; }
        public bool LOW_FUEL_25 { get; set; }
        public bool OVER_HEARTING_100 { get; set; }
        public bool HAS_LAT_LONG { get; set; }
        public bool IS_IN_DANGER { get; set; }
        public bool BEING_INTERDICTED { get; set; }
        public bool IN_MAINSHIP { get; set; }
        public bool IN_FIGHTER { get; set; }
        public bool IN_SRV { get; set; }
        public bool HUD_IN_ANALYSIS_MODE { get; set; }
        public bool NIGHT_VISION { get; set; }
        public bool ALTITUDE_FROM_AVARAGE_RADIUS { get; set; }
        public bool FSD_JUMP { get; set; }
        public bool SRV_HIGH_BEAM { get; set; }

        public void updateStatus(SerializeableStatus statusUpdate)
        {
            setProperty(new Pips(statusUpdate.Pips), this.Pips, nameof(this.Pips));
            setProperty((GuiFocus)statusUpdate.GuiFocus, this.GuiFocus, nameof(this.GuiFocus));
            setProperty(statusUpdate.FireGroup, this.FireGroup, nameof(this.FireGroup));
            setProperty(statusUpdate.Fuel, this.Fuel, nameof(this.Fuel));
            setProperty(statusUpdate.Cargo, this.Cargo, nameof(this.Cargo));
            setProperty(statusUpdate.Longitude, this.Longitude, nameof(this.Longitude));
            setProperty(statusUpdate.Latitude, this.Latitude, nameof(this.Latitude));
            setProperty(statusUpdate.Heading, this.Heading, nameof(this.Heading));
            setProperty(statusUpdate.Altitude, this.Altitude, nameof(this.Altitude));
            setProperty(statusUpdate.BodyName, this.BodyName, nameof(this.BodyName));
            setProperty(statusUpdate.PlanetRadius, this.PlanetRadius, nameof(this.PlanetRadius));

            Flags[] flags = (Flags[])Enum.GetValues(typeof(Flags));
            foreach (Flags flag in flags)
            {
                int mask = (int)flag;
                bool newState = (statusUpdate.Flags & mask) != 0;
                bool oldState = (bool)this.GetType().GetProperty(flag.ToString()).GetValue(this, null);
                string propertyName = flag.ToString();
                setProperty<bool>(newState, oldState, propertyName);
            }

            try
            {
                setProperty(LegalState.Parse<LegalState>(statusUpdate.LegalState, true), this.LegalState, nameof(this.LegalState));
            }
            catch (Exception)
            {
                setProperty(LegalState.NULL, this.LegalState, nameof(this.LegalState));
            }
        }
        private void setProperty<T>(T newProp, T oldProp, string name)
        {

            if (newProp == null)
            {
                if (oldProp == null)
                {
                    return;
                }
                this.GetType().GetProperty(name).SetValue(this, null);
                propertyChanged(name);
                return;
            }

            if (oldProp == null)
            {
                this.GetType().GetProperty(name).SetValue(this, newProp);
                propertyChanged(name);
                return;
            }
            else
            {
                if (!oldProp.Equals(newProp))
                {
                    this.GetType().GetProperty(name).SetValue(this, newProp);
                    propertyChanged(name);
                    return;
                }
            }
        }

        #region Singelton
        private static readonly Status instance = new Status();

        private Status() { }
        public static Status Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
