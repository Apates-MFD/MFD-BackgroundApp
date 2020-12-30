using System;
using System.Collections.Generic;

namespace EDLibrary.EDStatusWatcher
{
    public class SerializeableStatus
    {
        public DateTime timestamp { get; set; }
        public string @event { get; set; }
        public int Flags { get; set; }
        public List<int> Pips { get; set; }
        public int FireGroup { get; set; }
        public Fuel Fuel { get; set; }
        public double Cargo { get; set; }
        public string LegalState { get; set; }
        public int GuiFocus { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Heading { get; set; }
        public int Altitude { get; set; }
        public string BodyName { get; set; }
        public string PlanetRadius { get; set; }
    }
}
